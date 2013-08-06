#region License
// PaginatedCriteria.cs
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
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents the criteria used to show a paginated data.
    /// </summary>
    /// <remarks>
    /// The <see cref="PaginatedCriteria"/> properties are intentionally left without property change notification.
    /// </remarks>
    public class PaginatedCriteria : NotificationObject, IPaginatedCriteria
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedCriteria" /> class.
        /// </summary>
        public PaginatedCriteria()
        {
            this.PageSize = 10;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <remarks>
        /// Starts from 1.
        /// </remarks>
        [ScaffoldColumn(false)]
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        /// <remarks>
        /// The default value is 10.
        /// </remarks>
        [ScaffoldColumn(false)]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the sort descriptions.
        /// </summary>
        [ScaffoldColumn(false)]
        public SortDescription[] SortDescriptions { get; set; }

        /// <summary>
        /// Gets the start index according to the current page and page size.
        /// </summary>
        [ScaffoldColumn(false)]
        public int StartIndex
        {
            get
            {
                return ((this.Page.HasValue && this.Page > 0) ? this.Page.Value - 1 : 0) * this.PageSize;
            }
        }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        [ScaffoldColumn(false)]
        public string SortExpression
        {
            get
            {
                if (this.SortDescriptions == null)
                {
                    return string.Empty;
                }

                var sortExpressions = this.SortDescriptions
                    .Select(sd => sd.PropertyName + (sd.Direction == SortDirection.Descending ? "DESC" : string.Empty))
                    .ToArray();

                return string.Join(", ", sortExpressions);
            }

            set
            {
                if (value == null)
                {
                    this.SortDescriptions = null;
                    return;
                }

                this.SortDescriptions = this.ParseSortExpression(value).ToArray();
            }
        }

        /// <summary>
        /// Parses the specified sort expression.
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        private IEnumerable<SortDescription> ParseSortExpression(string sortExpression)
        {
            var sortExpressions = sortExpression.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sort in sortExpressions)
            {
                SortDescription sortDescription;
                if (SortDescription.TryParse(sort, out sortDescription))
                {
                    yield return sortDescription;
                }
            }
        }
    }
}
