#region License
// IPaginated.cs
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

namespace Xoqal.Web.Mvc.Models
{
    using System.Collections;

    /// <summary>
    /// Represents a paginated data.
    /// </summary>
    public interface IPaginated
    {
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value> The current page. </value>
        int CurrentPage { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        IEnumerable Data { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is first page.
        /// </summary>
        /// <value> <c>true</c> if this instance is first page; otherwise, <c>false</c> . </value>
        bool IsFirstPage { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        /// <value> <c>true</c> if this instance is last page; otherwise, <c>false</c> . </value>
        bool IsLastPage { get; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        int NextPage { get; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value> The size of the page. </value>
        int PageSize { get; }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        int PreviousPage { get; }

        /// <summary>
        /// Gets the total rows count.
        /// </summary>
        int TotalRowsCount { get; }
    }
}
