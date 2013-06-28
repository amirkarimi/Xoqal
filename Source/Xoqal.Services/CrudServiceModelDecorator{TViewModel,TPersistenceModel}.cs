#region License
// CrudServiceModelDecorator{TViewModel,TPersistenceModel}.cs
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
    /// Represents a model decorator for a <see cref="CrudService{TModel}" /> class.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TPersistenceModel">The type of the persistence model.</typeparam>
    public abstract class CrudServiceModelDecorator<TViewModel, TPersistenceModel> : CrudServiceModelDecorator<TViewModel, TPersistenceModel, PaginatedCriteria>
        where TViewModel : class
        where TPersistenceModel : class 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudServiceModelDecorator{TViewModel, TPersistenceModel}"/> class.
        /// </summary>
        /// <param name="persistenceService">The persistence service.</param>
        public CrudServiceModelDecorator(ICrudService<TPersistenceModel> persistenceService)
            : base(persistenceService)
        {
        }
    }
}
