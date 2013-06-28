#region License
// IPaginatedService{TModel, TCriteria}.cs
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
    /// Represents a paginated data service.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TCriteria">The type of the criteria.</typeparam>
    public interface IPaginatedService<TModel, in TCriteria> : IService
        where TModel : class
        where TCriteria : PaginatedCriteria
    {
        /// <summary>
        /// Gets an <see cref="IPaginated"/> instance according to the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPaginated<TModel> GetItems(TCriteria criteria);
    }
}
