#region License
// PresenterController{TModel}.cs
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
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Xoqal.Core.Models;
    using Xoqal.Data;
    using Xoqal.Services;

    /// <summary>
    /// Represents the base class for paginated data presenter controllers.
    /// </summary>
    public class PresenterController<TModel> : PresenterController<TModel, PaginatedCriteria, long>
        where TModel : class
    {
        private readonly IPresenterService<TModel> service;

        /// <summary>
        /// Initializes a new instance of the PresenterControllerresenterController{TModel,TCriteria} /> class.
        /// </summary>
        /// <param name="service"></param>
        public PresenterController(IPresenterService<TModel> service)
            : base(service)
        {
            this.service = service;
        }
    }
}
