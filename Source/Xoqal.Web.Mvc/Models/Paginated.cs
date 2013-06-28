#region License
// Paginated.cs
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
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a paginated data.
    /// </summary>
    public class Paginated<T> : IPaginated<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paginated{T}" /> class.
        /// </summary>
        /// <param name="data"> The data. </param>
        /// <param name="totalItemCount"> The total item count. </param>
        /// <param name="currentPage"> The current page. </param>
        /// <param name="pageSize"> Size of the page. </param>
        public Paginated(IEnumerable<T> data, int totalItemCount, int currentPage, int pageSize)
        {
            this.TotalRowsCount = totalItemCount;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
            this.Data = data;
        }

        #region IPaginated<T> Members

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value> The data. </value>
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        IEnumerable IPaginated.Data
        {
            get { return this.Data; }
        }

        /// <summary>
        /// Gets or sets the total rows count.
        /// </summary>
        /// <value> The total rows count. </value>
        public int TotalRowsCount { get; private set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value> The size of the page. </value>
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        /// <value> The index of the current page. </value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        public int PageCount
        {
            get { return (int)Math.Ceiling(this.TotalRowsCount / (double)this.PageSize); }
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        public int PreviousPage
        {
            get { return this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1; }
        }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        public int NextPage
        {
            get { return this.CurrentPage >= this.PageCount ? this.CurrentPage : this.CurrentPage + 1; }
        }

        /// <summary>
        /// Gets a value indicating whether the current page is the first page.
        /// </summary>
        /// <value> <c>true</c> if the current page is the first page; otherwise, <c>false</c> . </value>
        public bool IsFirstPage
        {
            get { return this.CurrentPage <= 1; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        /// <value> <c>true</c> if this instance is last page; otherwise, <c>false</c> . </value>
        public bool IsLastPage
        {
            get { return this.CurrentPage >= this.PageCount; }
        }

        #endregion
    }
}
