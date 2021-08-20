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
        /// 'Select' by condition before query executed in provider.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">the condition</param>
        /// <param name="trueExpression">The expression that will apply if condition is true.</param>
        /// <param name="falseExpression">The expression that will apply if condition is false.</param>
        /// <returns>
        /// filtered query
        /// </returns>
        public static IQueryable<TResult> SelectIf<TSource, TResult>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TResult>> trueExpression,
            Expression<Func<TSource, TResult>> falseExpression)
        {
            return condition ? source.Select(trueExpression) : source.Select(falseExpression);
        }

        /// <summary>
        /// Conditional 'Where', decide about predicate before query executed in provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">the condition</param>
        /// <param name="trueExpression">The expression that will apply if condition is true.</param>
        /// <param name="falseExpression">The expression that will apply if condition is false.</param>
        /// <returns>
        /// filtered query
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> trueExpression,
            Expression<Func<T, bool>> falseExpression)
        {
            return condition ? source.Where(trueExpression) : source.Where(falseExpression);
        }
    }
}
