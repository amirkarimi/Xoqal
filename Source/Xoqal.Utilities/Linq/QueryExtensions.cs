#region License
// QueryExtensions.cs
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

namespace Xoqal.Utilities.Linq
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// IQueryable Helpers
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// 'Select' by condition before query executed in provider
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <param name="firstPredicate">the predicate that execute if condition is true</param>
        /// <param name="secondPredicate">the predicate that execute if condition is false</param>
        /// <returns>filtered query</returns>
        public static IQueryable<TResult> SelectIf<TSource, TResult>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TResult>> firstPredicate,
            Expression<Func<TSource, TResult>> secondPredicate)
        {
            return condition ? source.Select(firstPredicate) : source.Select(secondPredicate);
        }

        /// <summary>
        /// Conditional 'Where', decide about predicate before query executed in provider
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <param name="firstPredicate">the predicate that execute if condition is true</param>
        /// <param name="secondPredicate">the predicate that execute if condition is false</param>
        /// <returns>filtered query</returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> firstPredicate,
            Expression<Func<T, bool>> secondPredicate)
        {
            return condition ? source.Where(firstPredicate) : source.Where(secondPredicate);
        }
    }
}
