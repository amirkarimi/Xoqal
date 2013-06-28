#region License
// IPaginatedCriteria.cs
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
    /// Represents the criteria used to show a paginated data.
    /// </summary>
    public interface IPaginatedCriteria : ICriteria
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <remarks>
        /// Starts from 1.
        /// </remarks>
        int? Page { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        /// The default value is 10.
        /// <remarks>
        /// </remarks>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the sort descriptions.
        /// </summary>
        SortDescription[] SortDescriptions { get; set; }

        /// <summary>
        /// Gets the start index according to the current page and page size.
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        string SortExpression { get; set; }
    }
}
