#region License
// DetailPaginatedCriteria{T}.cs
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

namespace Xoqal.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMasterModel">The type of the master model.</typeparam>
    /// <seealso cref="Xoqal.Core.Models.PaginatedCriteria" />
    public class DetailPaginatedCriteria<TMasterModel> : PaginatedCriteria
        where TMasterModel : class, new()
    {
        /// <summary>
        /// Gets or sets the master current item.
        /// </summary>
        /// <value> The master current item. </value>
        public TMasterModel MasterCurrentItem { get; set; }
    }
}
