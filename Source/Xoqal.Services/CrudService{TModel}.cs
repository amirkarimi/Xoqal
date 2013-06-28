#region License
// CrudService{TModel}.cs
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
    /// Represents the base functionality of a CRUD service.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class CrudService<TModel> : CrudService<TModel, PaginatedCriteria>, ICrudService<TModel>
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudService{TModel}" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public CrudService(IUnitOfWork unitOfWork, IRepository<TModel> repository)
            : base(unitOfWork, repository)
        {
        }

        /// <summary>
        /// Gets an <see cref="IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IPaginated<TModel> GetItems(PaginatedCriteria criteria)
        {
            var data = this.Repository.GetItems(criteria.StartIndex, criteria.PageSize, criteria.SortDescriptions);
            var totalRowsCount = this.Repository.GetItemCount();

            return new Paginated<TModel>(data, totalRowsCount);
        }
    }
}
