#region License
// PresenterController{TModel,TCriteria,TKey}.cs
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

namespace Xoqal.Web.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Xoqal.Services;
    using Xoqal.Web.Mvc.Extensions;
    using Xoqal.Web.Mvc.Models;

    /// <summary>
    /// Represents the base class for paginated data presenter controllers.
    /// </summary>
    public class PresenterController<TModel, TCriteria, TKey> : Controller, IPresenterController<TCriteria, TKey>
        where TModel : class
        where TCriteria : Core.Models.PaginatedCriteria, new()
    {
        private readonly IPresenterService<TModel, TCriteria> service;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterController{TModel, TCriteria, TKey}" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public PresenterController(IPresenterService<TModel, TCriteria> service)
        {
            this.service = service;
        }

        /// <summary>
        /// GET: /[Controller]/
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns> </returns>
        public virtual ActionResult Index(TCriteria criteria)
        {
            if (criteria == null)
            {
                criteria = new TCriteria();
            }

            return this.View(criteria);
        }

        /// <summary>
        /// GET: /[Controller]/Result/?page and sort Title DESC
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public virtual ActionResult Result(TCriteria criteria)
        {
            return this.PartialView(this.GetItems(criteria));
        }

        /// <summary>
        /// GET: /[Controller]/Details/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns></returns>
        public virtual ActionResult Details(TKey id)
        {
            return this.View(this.service.GetItemByKey(id));
        }

        /// <summary>
        /// Gets items matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        protected virtual IPaginated<TModel> GetItems(TCriteria criteria)
        {
            var paginatedData = this.service.GetItems(criteria);

            return paginatedData.ToPaginatedViewModel(criteria);
        }
    }
}
