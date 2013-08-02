#region License
// ICrudController{TModel,TKey}.cs
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

    /// <summary>
    /// Represents a controller which supports CRUD operations.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICrudController<in TModel, in TKey>
        where TModel : class
    {
        /// <summary>
        /// GET: /[Controller]/Create
        /// </summary>
        /// <returns> </returns>
        ActionResult Create();

        /// <summary>
        /// POST: /[Controller]/Create
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> </returns>
        ActionResult Create(TModel model);

        /// <summary>
        /// GET: /[Controller]/Remove/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns> </returns>
        ActionResult Delete(TKey id);

        /// <summary>
        /// GET: /[Controller]/Edit/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns> </returns>
        ActionResult Edit(TKey id);

        /// <summary>
        /// POST: /[Controller]/Edit/5
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="formCollection">The form collection.</param>
        /// <returns></returns>
        ActionResult Edit(TKey id, FormCollection formCollection);
    }
}
