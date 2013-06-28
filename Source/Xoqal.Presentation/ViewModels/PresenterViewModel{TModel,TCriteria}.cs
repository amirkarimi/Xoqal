#region License
// PresenterViewModel{TModel,TCriteria}.cs
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

namespace Xoqal.Presentation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xoqal.Core.Models;
    using Xoqal.Services;

    /// <summary>
    /// Represents a view-model to show data.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TCriteria">The type of the criteria.</typeparam>
    public class PresenterViewModel<TModel, TCriteria> : PresenterViewModelBase<TModel, TCriteria>
        where TModel : class
        where TCriteria : PaginatedCriteria, new()
    {
        #region Fields
        
        private readonly IPresenterService<TModel, TCriteria> service;
        
        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudViewModel&lt;TModel&gt;" /> class.
        /// </summary>
        /// <param name="service"></param>
        public PresenterViewModel(IPresenterService<TModel, TCriteria> service)
        {
            this.service = service;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the items matching the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        protected override IPaginated<TModel> GetItems(TCriteria criteria)
        {
            return this.service.GetItems(criteria);
        }

        #endregion
    }
}
