#region License
// CurrentPageChangedEventArgs.cs
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

    /// <summary>
    /// Provides data for CurrentPageChanged event.
    /// </summary>
    public class CurrentPageChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentPageChangedEventArgs" /> class.
        /// </summary>
        /// <param name="startIndex"> The index of the first item in the current page. </param>
        /// <param name="itemCount"> The count of items in the current page. </param>
        public CurrentPageChangedEventArgs(int startIndex, int itemCount)
        {
            this.StartIndex = startIndex;
            this.ItemCount = itemCount;
        }

        /// <summary>
        /// Gets the index of the first item in the current page.
        /// </summary>
        /// <value> The index of the first item. </value>
        public int StartIndex { get; private set; }

        /// <summary>
        /// Gets the count of items in the current page.
        /// </summary>
        /// <value> The item count. </value>
        public int ItemCount { get; private set; }
    }
}
