#region License
// PresenterViewModel{TModel}.cs
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
    using System.Text;
    using Xoqal.Core.Models;
    using Xoqal.Services;

    /// <summary>
    /// Represents a view-model to show data.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class PresenterViewModel<TModel> : PresenterViewModel<TModel, PaginatedCriteria>
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudViewModel&lt;TModel&gt;" /> class.
        /// </summary>
        /// <param name="service"></param>
        public PresenterViewModel(IPresenterService<TModel> service)
            : base(service)
        {
        }
    }
}
