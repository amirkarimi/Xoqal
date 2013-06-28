#region License
// PagerController.cs
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
    using System.Linq;
    using System.Windows.Input;
    using Microsoft.Practices.Prism.Commands;
    using Xoqal.Core.Models;

    /// <summary>
    /// Default implementation of the <see cref="IPagerControler"/>.
    /// </summary>
    public class PagerController : NotificationObject, IPagerController
    {
        /// <summary>
        /// The current page.
        /// </summary>
        private int currentPage;

        /// <summary>
        /// The length (number of items) of each page.
        /// </summary>
        private int pageSize;

        /// <summary>
        /// The count of items to be divided into pages.
        /// </summary>
        private int totalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerController" /> class.
        /// </summary>
        /// <param name="pageSize"> Size of the page. </param>
        /// <param name="totalCount"> The total count. </param>
        public PagerController(int pageSize, int totalCount = 1)
        {
            if (totalCount < 0)
            {
                throw new ArgumentException("Total count should be bigger than or equal to zero.", "totalCount");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size should be bigger than or equal to zero.", "pageSize");
            }

            this.totalCount = totalCount;
            this.pageSize = pageSize;
            this.currentPage = this.totalCount == 0 ? 0 : 1;

            this.GotoFirstPageCommand = new DelegateCommand(() => this.CurrentPage = 1, () => this.TotalCount != 0 && this.CurrentPage > 1);
            this.GotoLastPageCommand = new DelegateCommand(() => this.CurrentPage = this.PageCount, () => this.TotalCount != 0 && this.CurrentPage < this.PageCount);
            this.GotoNextPageCommand = new DelegateCommand(() => ++this.CurrentPage, () => this.TotalCount != 0 && this.CurrentPage < this.PageCount);
            this.GotoPreviousPageCommand = new DelegateCommand(() => --this.CurrentPage, () => this.TotalCount != 0 && this.CurrentPage > 1);
        }

        #region IPagerController Members

        /// <summary>
        /// Occurs when the value of <see cref="CurrentPage" /> changes.
        /// </summary>
        public event EventHandler<CurrentPageChangedEventArgs> CurrentPageChanged;

        /// <summary>
        /// Gets the command that, when executed, sets <see cref="CurrentPage" /> to 1.
        /// </summary>
        /// <value> The command that changes the current page. </value>
        public ICommand GotoFirstPageCommand { get; private set; }

        /// <summary>
        /// Gets the command that, when executed, decrements <see cref="CurrentPage" /> by 1.
        /// </summary>
        /// <value> The command that changes the current page. </value>
        public ICommand GotoPreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets the command that, when executed, increments <see cref="CurrentPage" /> by 1.
        /// </summary>
        /// <value> The command that changes the current page. </value>
        public ICommand GotoNextPageCommand { get; private set; }

        /// <summary>
        /// Gets the command that, when executed, sets <see cref="CurrentPage" /> to <see cref="PageCount" /> .
        /// </summary>
        /// <value> The command that changes the current page. </value>
        public ICommand GotoLastPageCommand { get; private set; }

        /// <summary>
        /// Gets or sets the total number of items to be divided into pages.
        /// </summary>
        /// <value> The total count. </value>
        public int TotalCount
        {
            get
            {
                return this.totalCount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total count should be bigger than or equal to zero");
                }

                this.totalCount = value;
                this.RaisePropertyChanged(() => this.TotalCount);
                this.RaisePropertyChanged(() => this.PageCount);
                RaiseCanExecuteChanged(this.GotoLastPageCommand, this.GotoNextPageCommand);

                if (this.CurrentPage > this.PageCount)
                {
                    this.CurrentPage = this.PageCount;
                }

                if (this.PageCount > 0 && this.CurrentPage == 0)
                {
                    this.CurrentPage = 1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of items that each page contains.
        /// </summary>
        /// <value> The size of the page. </value>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Page size should be bigger than or equal to zero.");
                }

                int oldStartIndex = this.CurrentPageStartIndex;
                this.pageSize = value;
                this.RaisePropertyChanged(() => this.PageSize);
                this.RaisePropertyChanged(() => this.PageCount);
                this.RaisePropertyChanged(() => this.CurrentPageStartIndex);
                RaiseCanExecuteChanged(this.GotoLastPageCommand, this.GotoNextPageCommand);

                if (oldStartIndex >= 0)
                {
                    this.CurrentPage = this.GetPageFromIndex(oldStartIndex);
                }
            }
        }

        /// <summary>
        /// Gets the number of pages required to contain all items.
        /// </summary>
        /// <value> The page count. </value>
        public int PageCount
        {
            get
            {
                if (this.totalCount == 0)
                {
                    return 0;
                }

                var ceil = (int)Math.Ceiling((double)this.totalCount / this.pageSize);
                return ceil;
            }
        }

        /// <summary>
        /// Gets or sets the current page starts from 1.
        /// </summary>
        /// <value> The current page. </value>
        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                if (value != 0 && this.PageCount == 0)
                {
                    value = 0;
                }

                if (value < 1 && this.PageCount != 0)
                {
                    value = 1;
                }

                if (value > this.PageCount)
                {
                    value = this.PageCount;
                }

                this.currentPage = value;
                this.RaisePropertyChanged(() => this.CurrentPage);
                this.RaisePropertyChanged(() => this.CurrentPageStartIndex);
                RaiseCanExecuteChanged(this.GotoLastPageCommand, this.GotoNextPageCommand);
                RaiseCanExecuteChanged(this.GotoFirstPageCommand, this.GotoPreviousPageCommand);

                this.OnCurrentPageChanged();
            }
        }

        /// <summary>
        /// Gets the index of the first item belonging to the current page.
        /// </summary>
        /// <value> The index of the first item belonging to the current page. </value>
        public int CurrentPageStartIndex
        {
            get { return this.PageCount == 0 ? -1 : (this.CurrentPage - 1) * this.PageSize; }
        }

        #endregion

        /// <summary>
        /// Calls RaiseCanExecuteChanged on any number of DelegateCommand instances.
        /// </summary>
        /// <param name="commands"> The commands. </param>
        private static void RaiseCanExecuteChanged(params ICommand[] commands)
        {
            foreach (DelegateCommand command in commands.Cast<DelegateCommand>())
            {
                command.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Called when current page changed.
        /// </summary>
        private void OnCurrentPageChanged()
        {
            EventHandler<CurrentPageChangedEventArgs> handler = this.CurrentPageChanged;
            if (handler != null)
            {
                handler(this, new CurrentPageChangedEventArgs(this.CurrentPageStartIndex, this.PageSize));
            }
        }

        /// <summary>
        /// Gets the number of the page to which the item with the specified index belongs.
        /// </summary>
        /// <param name="itemIndex"> The index of the item in question. </param>
        /// <returns> The number of the page in which the item with the specified index belongs. </returns>
        private int GetPageFromIndex(int itemIndex)
        {
            if (itemIndex < 0)
            {
                throw new ArgumentException("Item index could not be less than zero.", "itemIndex");
            }

            if (itemIndex > this.totalCount)
            {
                throw new ArgumentException("Item index could not be bigger than total item count.", "itemIndex");
            }

            int result = (int)Math.Floor((double)itemIndex / this.PageSize) + 1;
            return result;
        }
    }
}
