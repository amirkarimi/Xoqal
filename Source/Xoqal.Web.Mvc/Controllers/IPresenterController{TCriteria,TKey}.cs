#region License
// IPresenterController{TCriteria,TKey}.cs
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
    using System.Web.Mvc;
    using Models;
    using Xoqal.Core.Models;

    /// <summary>
    /// Represents a data presenter controller.
    /// </summary>
    public interface IPresenterController<in TCriteria, in TKey>
        where TCriteria : PaginatedCriteria, new()
    {
        /// <summary>
        /// GET: /[Controller]/Details/1
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> </returns>
        ActionResult Details(TKey id);

        /// <summary>
        /// GET: /[Controller]
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns> </returns>
        ActionResult Index(TCriteria criteria);

        /// <summary>
        /// GET: /[Controller]/Result/?page and sort
        /// </summary>
        /// <param name="criteria"> The criteria. </param>
        /// <returns> </returns>
        ActionResult Result(TCriteria criteria);
    }
}
