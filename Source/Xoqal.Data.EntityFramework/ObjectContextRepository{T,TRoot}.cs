#region License
// ObjectContextRepository{T,TRoot}.cs
// 
// Copyright (c) 2012 Xoqal.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace Xoqal.Data.EntityFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Metadata.Edm;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;
    using Xoqal.Core;
    using Xoqal.Data.Linq;

    /// <summary>
    /// Represents the base class for a repository.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    /// <typeparam name="TRoot">The root type.</typeparam>
    public abstract class ObjectContextRepository<T, TRoot> : IQueryableRepository<T>
        where TRoot : class
        where T : class, TRoot
    {
        #region Fields

        private readonly ObjectContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextRepository{T}" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public ObjectContextRepository(ObjectContext context)
        {
            this.context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the object set.
        /// </summary>
        public virtual ObjectSet<TRoot> ObjectSet
        {
            get { return this.Context.CreateObjectSet<TRoot>(); }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public virtual IQueryable<T> Query
        {
            get { return this.ObjectSet.OfType<T>().AsQueryable(); }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        IQueryable IQueryableRepository.Query
        {
            get { return this.Query; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected ObjectContext Context
        {
            get { return this.context; }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        public virtual IEnumerable<T> GetAllItems()
        {
            return this.Query;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="itemCount"> The item count. </param>
        /// <param name="sortDescirptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public IEnumerable<T> GetItems(int startIndex, int itemCount, SortDescription[] sortDescirptions = null)
        {
            return this.Query.ToPage(startIndex, itemCount, sortDescirptions);
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns> </returns>
        public int GetItemCount()
        {
            return this.Query.Count();
        }

        /// <summary>
        /// Gets the item by key.
        /// </summary>
        /// <param name="keys"> The keys. </param>
        /// <returns> </returns>
        public T GetItemByKey(params object[] keys)
        {
            ReadOnlyMetadataCollection<EdmMember> keyMembers = this.ObjectSet.EntitySet.ElementType.KeyMembers;

            IQueryable<T> query = this.Query;
            for (int i = 0; i < keys.Length; i++)
            {
                string propertyName = keyMembers[i].Name;
                query = query.Where(propertyName + " = @0", keys[i]);
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        IEnumerable IRepository.GetAllItems()
        {
            return this.GetAllItems();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="itemCount"> The item count. </param>
        /// <param name="sortDescirptions"> The sort descriptions. </param>
        /// <returns> </returns>
        IEnumerable IRepository.GetItems(int startIndex, int itemCount, SortDescription[] sortDescirptions)
        {
            return this.GetItems(startIndex, itemCount, sortDescirptions);
        }

        /// <summary>
        /// Gets an item by its key(s).
        /// </summary>
        /// <param name="keys"> The key(s). </param>
        /// <returns> </returns>
        object IRepository.GetItemByKey(params object[] keys)
        {
            return this.GetItemByKey(keys);
        }

        #endregion

        #region CUD Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        /// <summary>
        /// Updates the specified entity in both disconnected and connected manner.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Update(T entity)
        {
            ObjectStateEntry entry;
            bool hasEntry = this.Context.ObjectStateManager.TryGetObjectStateEntry(entity, out entry);
            if (hasEntry)
            {
                entry.ChangeState(EntityState.Modified);
            }
            else
            {
                this.Attach(entity);
                this.Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Remove(T entity)
        {
            // TODO: Add disconnected behavior
            var entry = this.Context.ObjectStateManager.GetObjectStateEntry(entity);
            if (entry.State == EntityState.Added)
            {
                this.ObjectSet.Detach(entity);
                return;
            }

            this.ObjectSet.DeleteObject(entity);
        }

        /// <summary>
        /// Deletes the items which match the specified predicate.
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        public virtual void Remove(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.Query.Where(predicate))
            {
                this.Remove(entity);
            }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void IRepository.Add(object entity)
        {
            this.Add((T)entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void IRepository.Update(object entity)
        {
            this.Update((T)entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void IRepository.Remove(object entity)
        {
            this.Remove((T)entity);
        }

        #endregion

        #region Attach/Detach Methods

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public void Attach(T entity)
        {
            ObjectStateEntry entry;
            bool hasEntry = this.Context.ObjectStateManager.TryGetObjectStateEntry(entity, out entry);
            if (hasEntry && entry.State != EntityState.Detached)
            {
                return;
            }

            this.ObjectSet.Attach(entity);
        }

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public void Detach(T entity)
        {
            ObjectStateEntry entry;
            bool hasEntry = this.Context.ObjectStateManager.TryGetObjectStateEntry(entity, out entry);
            if (hasEntry && entry.State != EntityState.Detached)
            {
                this.ObjectSet.Detach(entity);
            }
        }

        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual T Reload(T entity)
        {
            this.Context.Refresh(RefreshMode.StoreWins, entity);
            return entity;
        }

        /// <summary>
        /// Refreshes the specified refresh mode.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        object IRepository.Reload(object entity)
        {
            return this.Reload((T)entity);
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void IRepository.Attach(object entity)
        {
            this.Attach((T)entity);
        }

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void IRepository.Detach(object entity)
        {
            this.Detach((T)entity);
        }

        #endregion
    }
}
