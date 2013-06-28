#region License
// ListHelper.cs
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

namespace Xoqal.Presentation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the helpers to present the list items.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// Converts to a list item view model structure.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"> </param>
        /// <param name="textSelector"> </param>
        /// <param name="addEmptyItem"> if set to <c>true</c> add an empty item to the source. </param>
        /// <param name="emptyText"> </param>
        /// <returns> </returns>
        public static IEnumerable<ListItemViewModel> ToListItemViewModel<T>(
            this IEnumerable<T> source, Func<T, string> textSelector, bool addEmptyItem = false, string emptyText = "")
        {
            IEnumerable<ListItemViewModel> items = source.Select(s => new ListItemViewModel { Data = s, Text = textSelector(s) });

            if (addEmptyItem)
            {
                // Insert empty item
                items = (new[] { new ListItemViewModel { Text = emptyText } }).Concat(items);
            }

            return items;
        }

        /// <summary>
        /// Converts to a list item view model structure.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="textSelector"> </param>
        /// <param name="addEmptyItem"> if set to <c>true</c> add an empty item to the source. </param>
        /// <param name="source"> </param>
        /// <param name="dataSelector"> </param>
        /// <param name="emptyText"> </param>
        /// <returns> </returns>
        public static IEnumerable<ListItemViewModel> ToListItemViewModel<T>(
            this IEnumerable<T> source,
            Func<T, object> dataSelector,
            Func<T, string> textSelector,
            bool addEmptyItem = false,
            string emptyText = "")
        {
            IEnumerable<ListItemViewModel> items =
                source.Select(s => new ListItemViewModel { Data = dataSelector(s), Text = textSelector(s) });

            if (addEmptyItem)
            {
                // Insert empty item
                items = (new[] { new ListItemViewModel { Text = emptyText } }).Concat(items);
            }

            return items;
        }

        /// <summary>
        /// Converts to a simple text collection structure.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"> The source. </param>
        /// <param name="textSelector"> The text selector. </param>
        /// <param name="addEmptyItem"> if set to <c>true</c> [add empty item]. </param>
        /// <param name="emptyText"> The empty text. </param>
        /// <returns> </returns>
        public static IEnumerable<string> ToSimpleText<T>(
            this IEnumerable<T> source, Func<T, string> textSelector, bool addEmptyItem = false, string emptyText = "")
        {
            IEnumerable<string> items = source.Select(textSelector);

            if (addEmptyItem)
            {
                // Insert empty item
                items = (new[] { emptyText }).Concat(items);
            }

            return items;
        }
    }
}
