#region License
// CrudController{TModel,TCriteria,TKey}.cs
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
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Xoqal.Core.Models;
    using Xoqal.Services;

    /// <summary>
    /// Represents the base class for CRUD controllers.
    /// </summary>
    public class CrudController<TModel, TCriteria, TKey> : PresenterController<TModel, TCriteria, TKey>, ICrudController<TModel, TKey>
        where TModel : class, new()
        where TCriteria : PaginatedCriteria, new()
    {
        #region Fields

        private readonly ICrudService<TModel, TCriteria> service;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudController{TModel, TCriteria}" /> class.
        /// </summary>
        /// <param name="service"></param>
        public CrudController(ICrudService<TModel, TCriteria> service)
            : base(service)
        {
            this.service = service;
        }

        #endregion

        #region ICrudController<TModel> Members

        /// <summary>
        /// GET: /[Controller]/Create
        /// </summary>
        /// <returns> </returns>
        public virtual ActionResult Create()
        {
            return this.View(new TModel());
        }

        /// <summary>
        /// POST: /[Controller]/Create
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Create(TModel model)
        {
            this.service.Add(model);
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// GET: /[Controller]/Edit/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns> </returns>
        public virtual ActionResult Edit(TKey id)
        {
            return this.View(this.service.GetItemByKey(id));
        }

        /// <summary>
        /// POST: /[Controller]/Edit/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Edit(TKey id, TModel model)
        {
            var updatingModel = this.service.GetItemByKey(id);
            this.TryUpdateModel(updatingModel);
            this.service.Update(updatingModel);
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// GET: /[Controller]/Delete/5
        /// </summary>
        /// <param name="id"> The ID. </param>
        /// <returns> </returns>
        public virtual ActionResult Delete(TKey id)
        {
            try
            {
                TModel model = this.service.GetItemByKey(id);
                if (model != null)
                {
                    this.service.Remove(model);
                }

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException.Number == 547)
                {
                    return this.GetDeleteConflictErrorResult();
                }

                throw;
            }
        }

        /// <summary>
        /// Gets the delete conflict error result.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetDeleteConflictErrorResult()
        {
            return this.View("DeleteConflictError");
        }

        #endregion
    }
}
