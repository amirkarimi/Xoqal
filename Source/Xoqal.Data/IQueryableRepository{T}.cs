#region License
// IQueryableRepository{T}.cs
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

namespace Xoqal.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a LINQ supported repository.
    /// </summary>
    public interface IQueryableRepository<T> : IRepository<T>, IQueryableRepository
    {
        /// <summary>
        /// Gets the query.
        /// </summary>
        new IQueryable<T> Query { get; }

        /// <summary>
        /// Removes the items which match the specified predicate.
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        void Remove(Expression<Func<T, bool>> predicate);
    }
}
