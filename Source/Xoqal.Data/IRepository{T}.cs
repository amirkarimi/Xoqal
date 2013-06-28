#region License
// IRepository{T}.cs
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
    using System.Collections.Generic;
    using Xoqal.Core;

    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="T"> Entity type </typeparam>
    public interface IRepository<T> : IRepository
    {
        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        new IEnumerable<T> GetAllItems();

        /// <summary>
        /// Gets paginated items with support of sorting.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="itemCount">The item count.</param>
        /// <param name="sortDescriptions">The sort descriptions.</param>
        /// <returns></returns>
        new IEnumerable<T> GetItems(int startIndex, int itemCount, SortDescription[] sortDescriptions = null);

        /// <summary>
        /// Gets an item by its key(s).
        /// </summary>
        /// <param name="keys"> The key(s). </param>
        /// <returns> </returns>
        new T GetItemByKey(params object[] keys);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Attach(T entity);

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Detach(T entity);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Add(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Update(T entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Remove(T entity);

        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        T Reload(T entity);
    }
}
