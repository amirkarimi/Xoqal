#region License
// PresenterViewModelBase{TModel,TCriteria}.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Microsoft.Practices.Prism;
    using Xoqal.Core;
    using Xoqal.Core.Models;
    using Xoqal.Presentation.Commands;
    using Xoqal.Presentation.Utilities;
    using SortDescription = Xoqal.Core.SortDescription;

    /// <summary>
    /// Represents the base class for data presenting view-model.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TCriteria"></typeparam>
    public abstract class PresenterViewModelBase<TModel, TCriteria> : ViewModelBase, IPresenterViewModel<TModel, TCriteria>
        where TModel : class
        where TCriteria : PaginatedCriteria, new()
    {
        #region Fields

        private readonly ObservableCollection<TModel> entities;
        private readonly DataPresenterCollectionView view;
        private TModel currentItem;
        private TCriteria criteria;
        private IList selectedItems;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PresenterViewModelBase" /> class.
        /// </summary>
        public PresenterViewModelBase()
        {
            this.criteria = new TCriteria();
            this.criteria.PropertyChanged += this.OnCriteriaPropertyChanged;
            this.entities = new ObservableCollection<TModel>();
            this.view = new DataPresenterCollectionView(this.Entities);
            this.view.Refreshed += this.OnCollectionViewRefreshed;

            this.InitializePager();
            this.InitializeCommands();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when CurrentItem changed.
        /// </summary>
        public event EventHandler<EventArgs> CurrentItemChanged;

        /// <summary>
        /// Occurs before CurrentItem change.
        /// </summary>
        public event EventHandler<EventArgs> CurrentItemChanging;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the refresh command.
        /// </summary>
        /// <value> The refresh command. </value>
        public ICommand RefreshCommand { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value> The size of the page. </value>
        public virtual int PageSize
        {
            get { return 20; }
        }

        /// <summary>
        /// Gets the data (models).
        /// </summary>
        public ICollectionView Data
        {
            get { return this.view; }
        }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        public TCriteria Criteria
        {
            get
            {
                return this.criteria;
            }

            set
            {
                this.criteria = value;
                this.RaisePropertyChanged(() => this.Criteria);
            }
        }

        /// <summary>
        /// Gets or sets the current item.
        /// </summary>
        /// <value> The current item. </value>
        object IItemCollectionViewModel.CurrentItem
        {
            get { return this.CurrentItem; }
            set { this.CurrentItem = (TModel)value; }
        }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value> The selected items. </value>
        public IList SelectedItems
        {
            get
            {
                return this.selectedItems;
            }

            set
            {
                if (this.selectedItems == value)
                {
                    return;
                }

                this.selectedItems = value;
                this.RaisePropertyChanged(() => this.SelectedItems);
            }
        }

        /// <summary>
        /// Gets the pager.
        /// </summary>
        public PagerController Pager { get; private set; }

        /// <summary>
        /// Gets the current item.
        /// </summary>
        /// <value> The current item. </value>
        public TModel CurrentItem
        {
            get
            {
                return this.currentItem;
            }

            set
            {
                if (this.currentItem == value)
                {
                    return;
                }

                this.OnCurrentItemChanging(value);

                // Detach old object events
                this.DetachPropertiesChangedEvent(this.currentItem as INotifyPropertyChanged);
                this.currentItem = value;
                
                // Attach new object events
                this.AttachPropertiesChangedEvent(this.currentItem as INotifyPropertyChanged);

                this.RaisePropertyChanged(() => this.CurrentItem);
                this.OnCurrentItemChanged();

                this.RaisePropertyChanged(() => this.HasCurrentItem);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has current item.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has current item; otherwise, <c>false</c>.
        /// </value>
        public bool HasCurrentItem
        {
            get { return this.CurrentItem != null; }
        }

        /// <summary>
        /// Gets the models.
        /// </summary>
        protected ObservableCollection<TModel> Entities
        {
            get { return this.entities; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the data.
        /// </summary>
        public virtual void UpdateData()
        {
            if (DesignTime.IsInDesignMode)
            {
                return;
            }

            // Update the criteria paging and sorting information
            this.criteria.Page = this.Pager.CurrentPage;
            this.criteria.PageSize = this.Pager.PageSize;
            this.criteria.SortDescriptions = this.GetDataSortDescriptions(this.view.SortDescriptions.ToArray());
                
            // Fetch data
            var paginatedData = this.GetItems(this.criteria);

            // Update view-model
            this.Pager.TotalCount = paginatedData.TotalRowsCount;
            this.Entities.Clear();
            this.Entities.AddRange(paginatedData.Data);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the items matching the specified criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        protected abstract IPaginated<TModel> GetItems(TCriteria criteria);

        /// <summary>
        /// Called before CurrentItem change.
        /// </summary>
        protected virtual void OnCurrentItemChanging(TModel newValue)
        {
            EventHandler<EventArgs> handler = this.CurrentItemChanging;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when CurrentItem changed.
        /// </summary>
        protected virtual void OnCurrentItemChanged()
        {
            EventHandler<EventArgs> handler = this.CurrentItemChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when RefreshCommand execute.
        /// </summary>
        protected virtual void OnRefreshCommandExecute()
        {
            this.UpdateData();
        }

        /// <summary>
        /// Determines whether this instance can execute RefreshCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute RefreshCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteRefreshCommand()
        {
            return true;
        }

        /// <summary>
        /// Called when properties of current item changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data. </param>
        protected virtual void OnCurrentItemPropertiesChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Called when criteria property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCriteriaPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateData();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when collection view refreshed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCollectionViewRefreshed(object sender, EventArgs e)
        {
            this.UpdateData();
        }

        /// <summary>
        /// Initializes the pager.
        /// </summary>
        private void InitializePager()
        {
            this.Pager = new PagerController(this.PageSize);
            this.Pager.CurrentPageChanged += (s, e) => this.UpdateData();
        }

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitializeCommands()
        {
            this.RefreshCommand = new RelayCommand(this.OnRefreshCommandExecute, this.CanExecuteRefreshCommand);
        }

        /// <summary>
        /// Converts WPF sort description objects to Fx ones.
        /// </summary>
        /// <param name="sortDescriptions"> The sort descriptions. </param>
        /// <returns> </returns>
        private SortDescription[] GetDataSortDescriptions(IEnumerable<System.ComponentModel.SortDescription> sortDescriptions)
        {
            return
                sortDescriptions
                    .Select(s => 
                        new SortDescription(s.PropertyName, (SortDirection)Enum.Parse(typeof(SortDirection), s.Direction.ToString())))
                    .ToArray();
        }

        /// <summary>
        /// Attaches to the PropertyChanged of the given object.
        /// </summary>
        /// <param name="notifyObject"> </param>
        private void AttachPropertiesChangedEvent(INotifyPropertyChanged notifyObject)
        {
            if (notifyObject == null)
            {
                return;
            }

            notifyObject.PropertyChanged += this.OnCurrentItemPropertiesChanged;
        }

        /// <summary>
        /// Attaches to the PropertyChanged of the given object.
        /// </summary>
        /// <param name="notifyObject"> </param>
        private void DetachPropertiesChangedEvent(INotifyPropertyChanged notifyObject)
        {
            if (notifyObject == null)
            {
                return;
            }

            notifyObject.PropertyChanged -= this.OnCurrentItemPropertiesChanged;
        }

        #endregion
    }
}
