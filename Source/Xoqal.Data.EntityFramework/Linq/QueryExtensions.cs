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

namespace Xoqal.Data.EntityFramework.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Linq.Expressions;

    /// <summary>
    /// IQueryable extensions.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TProp"> The type of the prop. </typeparam>
        /// <param name="q"> The query. </param>
        /// <param name="expr"> The expression. </param>
        /// <returns> </returns>
        public static ObjectQuery<T> Include<T, TProp>(this ObjectQuery<T> q, Expression<Func<T, TProp>> expr)
        {
            return q.Include(NameOf<T>.Property(expr));
        }

        /// <summary>
        /// Includes the specified expression, but works on <see cref="IQueryable"/> which supposed that is <see cref="ObjectQuery"/>.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TProp"> The type of the prop. </typeparam>
        /// <param name="q"> The q. </param>
        /// <param name="expr"> The expression. </param>
        /// <returns> </returns>
        public static IQueryable<T> Include<T, TProp>(this IQueryable<T> q, Expression<Func<T, TProp>> expr)
        {
            if (q is DbQuery<T>)
            {
                return ((DbQuery<T>)q).Include(NameOf<T>.Property(expr));
            }

            return ((ObjectQuery<T>)q).Include(NameOf<T>.Property(expr));
        }

        /// <summary>
        /// Includes the specified expression, but works on <see cref="IQueryable"/> which supposed that is <see cref="ObjectQuery"/>.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="q"> The query. </param>
        /// <param name="expr"> The expression. </param>
        /// <returns> </returns>
        public static IQueryable<T> Include<T>(this IQueryable<T> q, string expr)
        {
            if (q is DbQuery<T>)
            {
                return ((DbQuery<T>)q).Include(expr);
            }

            return ((ObjectQuery<T>)q).Include(expr);
        }

        /// <summary>
        /// Calls the ToList method of the given query.
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <param name="dataObjectType"> Type of the data object. </param>
        /// <returns> </returns>
        public static IList ToList(this IQueryable query, Type dataObjectType)
        {
            return
                (IList)
                    typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new[] { dataObjectType }).Invoke(null, new object[] { query });
        }

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
