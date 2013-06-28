#region License
// PresenterService{TModel,TCriteria}.cs
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
    using System.Linq;
    using System.Text;
    using Xoqal.Core.Models;
    using Xoqal.Data;

    /// <summary>
    /// Represents the base functionality of a service with sort and pagination support.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TCriteria"></typeparam>
    public abstract class PresenterService<TModel, TCriteria> : IPresenterService<TModel, TCriteria>
        where TModel : class
        where TCriteria : PaginatedCriteria
    {
        private readonly IRepository<TModel> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterService{TModel, TCriteria}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PresenterService(IRepository<TModel> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the default repository.
        /// </summary>
        protected virtual IRepository<TModel> Repository
        {
            get { return this.repository; }
        }

        /// <summary>
        /// Gets an <see cref="IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public abstract IPaginated<TModel> GetItems(TCriteria criteria);

        /// <summary>
        /// Gets an item by its key(s).
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual TModel GetItemByKey(params object[] keys)
        {
            return this.Repository.GetItemByKey(keys);
        }
    }
}
