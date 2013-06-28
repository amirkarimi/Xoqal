#region License
// PresenterServiceModelDecorator{TViewModel,TPersistenceModel,TCriteria}.cs
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

    /// <summary>
    /// Represents a model decorator for a <see cref="PresenterService{TModel, TCriteria}" /> class.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TPersistenceModel">The type of the persistence model.</typeparam>
    /// <typeparam name="TCriteria">The type of the criteria.</typeparam>
    public abstract class PresenterServiceModelDecorator<TViewModel, TPersistenceModel, TCriteria> : IPresenterService<TViewModel, TCriteria>, IMapper<TPersistenceModel, TViewModel>
        where TViewModel : class
        where TPersistenceModel : class
        where TCriteria : PaginatedCriteria
    {
        private readonly IPresenterService<TPersistenceModel, TCriteria> persistencePresenterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterServiceModelDecorator{TViewModel, TPersistenceModel, TCriteria}"/> class.
        /// </summary>
        /// <param name="persistencePresenterService">The persistence presenter service.</param>
        public PresenterServiceModelDecorator(IPresenterService<TPersistenceModel, TCriteria> persistencePresenterService)
        {
            this.persistencePresenterService = persistencePresenterService;
        }

        /// <summary>
        /// Gets an <see cref="IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public abstract IPaginated<TViewModel> GetItems(TCriteria criteria);

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public abstract TViewModel Map(TPersistenceModel source, TViewModel destination = null);

        /// <summary>
        /// Gets the item by key.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public TViewModel GetItemByKey(params object[] keys)
        {
            return this.Map(this.persistencePresenterService.GetItemByKey(keys));
        }
    }
}
