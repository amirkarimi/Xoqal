#region License
// DbContextRepository{T,TRoot}.cs
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using Xoqal.Core;
    using Xoqal.Data.Linq;

    /// <summary>
    /// Represents the base class for a repository.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    /// <typeparam name="TRoot">The root type.</typeparam>
    public abstract class DbContextRepository<T, TRoot> : IQueryableRepository<T> 
        where TRoot : class
        where T : class, TRoot
    {
        #region Fields

        private readonly DbContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextRepository{T}" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        protected DbContextRepository(DbContext context)
        {
            this.context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the DB set.
        /// </summary>
        public virtual DbSet<TRoot> DbSet
        {
            get { return this.Context.Set<TRoot>(); }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        public virtual IQueryable<T> Query
        {
            get { return this.DbSet.OfType<T>().AsQueryable(); }
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
        protected DbContext Context
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
            return (T)this.DbSet.Find(keys);
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

        #region CRUD Methods

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity in a disconnected manner.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Update(T entity)
        {
            DbEntityEntry<T> entry = this.Context.Entry(entity);

            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                var attachedEntity = this.GetAttachedEntity(entity);

                if (attachedEntity != null)
                {
                    var attachedEntry = this.Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = System.Data.Entity.EntityState.Modified; // This should attach entity
                }
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual void Remove(T entity)
        {
            DbEntityEntry<T> entry = this.Context.Entry(entity);

            if (entry.State == System.Data.Entity.EntityState.Added)
            {
                entry.State = System.Data.Entity.EntityState.Detached;
                return;
            }
            
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                entity = this.GetAttachedEntity(entity);
            }

            this.DbSet.Remove(entity);
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
            DbEntityEntry<T> entry = this.Context.Entry(entity);
            if (entry.State != System.Data.Entity.EntityState.Detached)
            {
                return;
            }

            this.DbSet.Attach(entity);
        }

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public void Detach(T entity)
        {
            this.Context.Entry(entity).State = System.Data.Entity.EntityState.Detached;
        }

        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public virtual T Reload(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                return this.GetAttachedEntity(entity);
            }

            entry.Reload();
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

        /// <summary>
        /// Gets the attached entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private T GetAttachedEntity(T entity)
        {
            var set = ((IObjectContextAdapter)this.Context).ObjectContext.CreateObjectSet<TRoot>();
            var entitySet = set.EntitySet;
            var entityType = entity.GetType();
            var keyValues = entitySet.ElementType.KeyMembers.Select(k => entityType.GetProperty(k.Name).GetValue(entity, null)).ToArray();

            var attachedEntity = (T)this.DbSet.Find(keyValues);
            return attachedEntity;
        }

        #endregion
    }
}
