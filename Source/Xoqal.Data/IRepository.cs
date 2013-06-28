#region License
// IRepository.cs
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
    using System.Collections;
    using Xoqal.Core;

    /// <summary>
    /// Represents a generic object repository.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns> </returns>
        IEnumerable GetAllItems();

        /// <summary>
        /// Gets paginated items supporting sorting.
        /// </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="itemCount"> The item count. </param>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        IEnumerable GetItems(int startIndex, int itemCount, SortDescription[] sortDescriptions = null);

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <returns> </returns>
        int GetItemCount();

        /// <summary>
        /// Gets an item by its key(s).
        /// </summary>
        /// <param name="keys"> The key(s). </param>
        /// <returns> </returns>
        object GetItemByKey(params object[] keys);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Attach(object entity);

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Detach(object entity);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Add(object entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Update(object entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        void Remove(object entity);

        /// <summary>
        /// Reloads the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        object Reload(object entity);
    }
}
