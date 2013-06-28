#region License
// MasterDetailViewModel.cs
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
    using Commands;

    /// <summary>
    /// Represents the base functionality for a master-detail view-model.
    /// </summary>
    public class MasterDetailViewModel<TMasterViewModel, TDetailViewModel> : ViewModelBase,
        IMasterDetailViewModel<TMasterViewModel, TDetailViewModel>
        where TMasterViewModel : class, IItemCollectionViewModel, IRefreshableViewModel
        where TDetailViewModel : class, IDetailViewModel, IRefreshableViewModel
    {
        private TDetailViewModel detailViewModel;
        private TMasterViewModel masterViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDetailViewModel&lt;TMasterViewModel, TDetailViewModel&gt;" /> class.
        /// </summary>
        /// <param name="masterViewModel"> The master view model. </param>
        /// <param name="detailViewModel"> The detail view model. </param>
        public MasterDetailViewModel(TMasterViewModel masterViewModel, TDetailViewModel detailViewModel)
        {
            this.MasterViewModel = masterViewModel;
            this.DetailViewModel = detailViewModel;

            var masterCrudViewModel = masterViewModel as ICrudViewModel;
            if (masterCrudViewModel != null)
            {
                masterCrudViewModel.EditModeChanged += this.OnMasterViewModelEditModeChanged;
            }

            var detailCrudViewModel = detailViewModel as ICrudViewModel;
            if (detailCrudViewModel != null)
            {
                detailCrudViewModel.EditModeChanged += this.OnDetailViewModelEditModeChanged;
            }

            this.RefreshCommand = new RelayCommand(this.OnRefreshCommandExecute);
            this.MasterViewModel.CurrentItemChanged += this.OnMasterViewModelCurrentItemChanged;
        }

        #region IMasterDetailViewModel<TMasterViewModel,TDetailViewModel> Members

        /// <summary>
        /// Gets the master view model.
        /// </summary>
        public TMasterViewModel MasterViewModel
        {
            get
            {
                return this.masterViewModel;
            }

            private set
            {
                if (this.masterViewModel == value)
                {
                    return;
                }

                this.masterViewModel = value;
                this.RaisePropertyChanged(() => this.MasterViewModel);
            }
        }

        /// <summary>
        /// Gets or sets the detail view model.
        /// </summary>
        /// <value> The detail view model. </value>
        public TDetailViewModel DetailViewModel
        {
            get
            {
                return this.detailViewModel;
            }

            private set
            {
                if (this.detailViewModel == value)
                {
                    return;
                }

                this.detailViewModel = value;
                this.RaisePropertyChanged(() => this.DetailViewModel);
            }
        }

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        public ICommand RefreshCommand { get; private set; }

        #endregion

        /// <summary>
        /// Called when RefreshCommand execute.
        /// </summary>
        protected virtual void OnRefreshCommandExecute()
        {
            this.MasterViewModel.RefreshCommand.Execute(null);
            this.DetailViewModel.RefreshCommand.Execute(null);
        }

        /// <summary>
        /// Handles the CurrentItemChanged event of the MasterViewModel control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.EventArgs" /> instance containing the event data. </param>
        private void OnMasterViewModelCurrentItemChanged(object sender, EventArgs e)
        {
            this.DetailViewModel.MasterCurrentItem = this.MasterViewModel.CurrentItem;
            this.DetailViewModel.RefreshCommand.Execute(null);
        }

        /// <summary>
        /// Handles the EditModeChanged event of the MasterViewModel control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.EventArgs" /> instance containing the event data. </param>
        private void OnMasterViewModelEditModeChanged(object sender, EventArgs e)
        {
            this.DetailViewModel.IsEnabled = !((ICrudViewModel)this.MasterViewModel).IsInEditMode;
        }

        /// <summary>
        /// Handles the EditModeChanged event of the DetailViewModel control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.EventArgs" /> instance containing the event data. </param>
        private void OnDetailViewModelEditModeChanged(object sender, EventArgs e)
        {
            this.MasterViewModel.IsEnabled = !((ICrudViewModel)this.DetailViewModel).IsInEditMode;
        }
    }
}
