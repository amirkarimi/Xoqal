#region License
// Extensions.cs
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

namespace Xoqal.Data.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Xoqal.Core;
    using Xoqal.Core.Models;

    public static class Extensions
    {
        /// <summary>
        /// Sorts the specified query.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortDescription[] sortDescriptions)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (sortDescriptions != null)
            {
                foreach (SortDescription sortDescription in sortDescriptions.Reverse())
                {
                    string property = sortDescription.PropertyName;
                    if (sortDescription.Direction == SortDirection.Descending)
                    {
                        property += " DESC";
                    }

                    query = query.OrderBy(property);
                }
            }

            return query;
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="startIndex"> Start index of the row. </param>
        /// <param name="itemCount"> Size of the page. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int startIndex, int itemCount, SortDescription[] sortDescriptions)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if (sortDescriptions != null)
            {
                foreach (SortDescription sortDescription in sortDescriptions.Reverse())
                {
                    string property = sortDescription.PropertyName;
                    if (sortDescription.Direction == SortDirection.Descending)
                    {
                        property += " DESC";
                    }

                    query = query.OrderBy(property);
                }
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            return query.Skip(startIndex).Take(itemCount);
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int? page, int pageSize, string sortExpression)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToPage(
                ((page ?? 1) - 1) * pageSize,
                pageSize,
                GetSortDescriptions(sortExpression).ToArray());
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">query</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize, paginatedCriteria.SortDescriptions);
        }

        /// <summary>
        /// Creates an <see cref="IPaginated{T}" /> instance from the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static IPaginated<T> ToPaginated<T>(this IQueryable<T> query, PaginatedCriteria criteria)
        {
            return new Paginated<T>(
                query.ToPage(criteria.StartIndex, criteria.PageSize, criteria.SortDescriptions),
                query.Count());
        }

        /// <summary>
        /// Gets the sort descriptions from the specified criteria.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        private static IEnumerable<SortDescription> GetSortDescriptions(string sort)
        {
            SortDescription sortDescription;
            if (SortDescription.TryParse(sort, out sortDescription))
            {
                yield return sortDescription;
            }
        }
    }
}
