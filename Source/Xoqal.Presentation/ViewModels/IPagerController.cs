#region License
// IPagerController.cs
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
    using System.Windows.Input;

    /// <summary>
    /// Represents a pager controller which helps to show a large amount of data in a paginated format.
    /// </summary>
    public interface IPagerController
    {
        /// <summary>
        /// Occurs when current page changed.
        /// </summary>
        event EventHandler<CurrentPageChangedEventArgs> CurrentPageChanged;

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value> The current page. </value>
        int CurrentPage { get; set; }

        /// <summary>
        /// Gets the start index of the current page.
        /// </summary>
        /// <value> The start index of the current page. </value>
        int CurrentPageStartIndex { get; }

        /// <summary>
        /// Gets the goto first page command.
        /// </summary>
        ICommand GotoFirstPageCommand { get; }

        /// <summary>
        /// Gets the goto last page command.
        /// </summary>
        ICommand GotoLastPageCommand { get; }

        /// <summary>
        /// Gets the goto next page command.
        /// </summary>
        ICommand GotoNextPageCommand { get; }

        /// <summary>
        /// Gets the goto previous page command.
        /// </summary>
        ICommand GotoPreviousPageCommand { get; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value> The size of the page. </value>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value> The total count. </value>
        int TotalCount { get; set; }
    }
}
