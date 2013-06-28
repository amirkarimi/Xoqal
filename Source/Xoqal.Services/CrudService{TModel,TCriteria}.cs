#region License
// CrudService{TModel,TCriteria}.cs
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

namespace Xoqal.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Xoqal.Core.Models;
    using Xoqal.Data;

    /// <summary>
    /// Represents the base functionality of a CRUD service.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TCriteria"></typeparam>
    public abstract class CrudService<TModel, TCriteria> : PresenterService<TModel, TCriteria>, ICrudService<TModel, TCriteria>
        where TModel : class
        where TCriteria : PaginatedCriteria
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudService{TModel, TCriteria}" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public CrudService(IUnitOfWork unitOfWork, IRepository<TModel> repository)
            : base(repository)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        /// <summary>
        /// Adds the specified model. 
        /// </summary>
        /// <param name="model"></param>
        public virtual void Add(TModel model)
        {
            this.Repository.Add(model);
            this.UnitOfWork.Commit();
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model"></param>
        public virtual void Update(TModel model)
        {
            this.Repository.Update(model);
            this.UnitOfWork.Commit();
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model"></param>
        public virtual void Remove(TModel model)
        {
            try
            {
                this.Repository.Remove(model);
                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Deletes all the specified models.
        /// </summary>
        /// <param name="models"></param>
        public virtual void Remove(IEnumerable<TModel> models)
        {
            try
            {
                foreach (var model in models)
                {
                    this.Repository.Remove(model);
                }

                this.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && innerException.Number == 547)
                {
                    this.unitOfWork.RollBack();
                }

                throw;
            }
        }

        /// <summary>
        /// Reloads the specified model.
        /// </summary>
        /// <param name="model"></param>
        public virtual TModel Reload(TModel model)
        {
            return this.Repository.Reload(model);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TModel> GetAllItems()
        {
            return this.Repository.GetAllItems();
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns></returns>
        public int GetItemCount()
        {
            return this.Repository.GetItemCount();
        }
    }
}
