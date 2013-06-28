#region License
// CrudViewModel{TModel,TCriteria}.cs
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
    using System.Data.SqlClient;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Resources;
    using Xoqal.Core.Models;
    using Xoqal.Data;
    using Xoqal.Presentation.Commands;
    using Xoqal.Services;

    /// <summary>
    /// Represents the base functionality for a simple view-model CRUD operation.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TCriteria"></typeparam>
    public abstract class CrudViewModel<TModel, TCriteria> : PresenterViewModel<TModel, TCriteria>, ICrudViewModel<TModel, TCriteria>
        where TModel : class, new()
        where TCriteria : PaginatedCriteria, new()
    {
        #region Fields

        private readonly ICrudService<TModel, TCriteria> service;
        private EditMode editMode;
        private bool isRollingBackChanges;
        private IValidationController validationController;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudViewModel{TModel, TCriteria}" /> class.
        /// </summary>
        /// <param name="service"></param>
        protected CrudViewModel(ICrudService<TModel, TCriteria> service)
            : base(service)
        {
            this.service = service;
            this.InitializeCommands();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when edit mode changed.
        /// </summary>
        public event EventHandler EditModeChanged;

        #endregion

        #region Commands

        /// <summary>
        /// Gets the new item command.
        /// </summary>
        public ICommand AddCommand { get; private set; }

        /// <summary>
        /// Gets the edit item command.
        /// </summary>
        public ICommand EditCommand { get; private set; }

        /// <summary>
        /// Gets the delete item command.
        /// </summary>
        public ICommand DeleteCommand { get; private set; }

        /// <summary>
        /// Gets the accept changes command.
        /// </summary>
        public ICommand AcceptChangesCommand { get; private set; }

        /// <summary>
        /// Gets the cancel changes command.
        /// </summary>
        public ICommand CancelChangesCommand { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the edit mode.
        /// </summary>
        /// <value> The edit mode. </value>
        public EditMode EditMode
        {
            get
            {
                return this.editMode;
            }

            set
            {
                if (this.editMode == value)
                {
                    return;
                }

                this.editMode = value;
                this.RaisePropertyChanged(() => this.EditMode);
                this.RaisePropertyChanged(() => this.IsInEditMode);
                this.OnEditModeChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is in edit mode.
        /// </summary>
        /// <value> <c>true</c> if this instance is in edit mode; otherwise, <c>false</c> . </value>
        public bool IsInEditMode
        {
            get { return this.EditMode != EditMode.None; }
        }

        /// <summary>
        /// Gets the valuator controller.
        /// </summary>
        /// <value> The validation controller. </value>
        public IValidationController ValidationController
        {
            get { return this.validationController ?? (this.validationController = new ValidationController()); }
            set { this.validationController = value; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when AddCommand execute.
        /// </summary>
        protected virtual void OnAddCommandExecute()
        {
            this.CurrentItem = new TModel();
            this.EditMode = EditMode.Insert;
            this.Entities.Add(this.CurrentItem);
        }

        /// <summary>
        /// Determines whether this instance can execute the AddCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute the AddCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteAddCommand()
        {
            return !this.IsInEditMode;
        }

        /// <summary>
        /// Called when EditCommand execute.
        /// </summary>
        protected virtual void OnEditCommandExecute()
        {
            this.EditMode = EditMode.Edit;
        }

        /// <summary>
        /// Determines whether this instance can execute the EditCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute the EditCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteEditCommand()
        {
            return !this.IsInEditMode && this.CurrentItem != null;
        }

        /// <summary>
        /// Called when DeleteCommand execute.
        /// </summary>
        protected virtual void OnDeleteCommandExecute()
        {
            try
            {
                if (this.SelectedItems != null && this.SelectedItems.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show(
                        string.Format(
                            Resource.DeleteConfirm,
                            this.SelectedItems.Count),
                        Resource.Delete,
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        this.service.Remove(this.SelectedItems.Cast<TModel>());

                        foreach (TModel item in this.SelectedItems.Cast<TModel>().ToList())
                        {
                            this.Entities.Remove(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException.InnerException as SqlException;
                if (innerException.Number == 547)
                {
                    MessageBox.Show(
                        Resource.DeleteConflictErrorMessage,
                        Resource.Error,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Determines whether this instance can execute the DeleteCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute the DeleteCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteDeleteCommand()
        {
            return !this.IsInEditMode && this.CurrentItem != null;
        }

        /// <summary>
        /// Called when AcceptChangesCommand execute.
        /// </summary>
        protected virtual void OnAcceptChangesCommandExecute()
        {
            var currentItem = this.CurrentItem;
            var editMode = this.EditMode;

            switch (this.EditMode)
            {
                case EditMode.Edit:
                    this.service.Update(this.CurrentItem);
                    break;
                case EditMode.Insert:
                    this.service.Add(this.CurrentItem);
                    break;
            }

            this.EditMode = EditMode.None;
            this.UpdateData();

            if (editMode == EditMode.Edit)
            {
                this.CurrentItem = currentItem;
            }
            else
            {
                if (this.Entities.Contains(currentItem))
                {
                    this.CurrentItem = currentItem;
                }
            }
        }

        /// <summary>
        /// Determines whether this instance can execute the AcceptChangesCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute the AcceptChangesCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteAcceptChangesCommand()
        {
            return this.IsInEditMode && !this.ValidationController.HasError;
        }

        /// <summary>
        /// Called when CancelChangesCommand execute.
        /// </summary>
        protected virtual void OnCancelChangesCommandExecute()
        {
            this.RollBackChanges();
        }

        /// <summary>
        /// Determines whether this instance can execute the CancelChangesCommand.
        /// </summary>
        /// <returns> <c>true</c> if this instance can execute the CancelChangesCommand; otherwise, <c>false</c> . </returns>
        protected virtual bool CanExecuteCancelChangesCommand()
        {
            return this.IsInEditMode;
        }

        /// <summary>
        /// Called before CurrentItem change.
        /// </summary>
        protected override void OnCurrentItemChanging(TModel newValue)
        {
            base.OnCurrentItemChanging(newValue);

            if (newValue == null && this.IsInEditMode && !this.isRollingBackChanges)
            {
                // Cancel the edit mode if we are not in edit mode and the CurrentItem is set to null.
                this.RollBackChanges();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        private void InitializeCommands()
        {
            this.AddCommand = new RelayCommand(this.OnAddCommandExecute, this.CanExecuteAddCommand);
            this.EditCommand = new RelayCommand(this.OnEditCommandExecute, this.CanExecuteEditCommand);
            this.DeleteCommand = new RelayCommand(this.OnDeleteCommandExecute, this.CanExecuteDeleteCommand);
            this.AcceptChangesCommand = new RelayCommand(
                this.OnAcceptChangesCommandExecute,
                this.CanExecuteAcceptChangesCommand);
            this.CancelChangesCommand = new RelayCommand(
                this.OnCancelChangesCommandExecute,
                this.CanExecuteCancelChangesCommand);
        }

        /// <summary>
        /// Rolls the changes back (Cancels the edit mode) but doesn't change the EditMode value.
        /// </summary>
        private void RollBackChanges()
        {
            this.isRollingBackChanges = true;

            if (this.CurrentItem != null)
            {
                if (this.EditMode == EditMode.Edit)
                {
                    // TODO: Need test
                    this.CurrentItem = this.service.Reload(this.CurrentItem);
                }
                else
                {
                    this.Entities.Remove(this.CurrentItem);
                    this.CurrentItem = null;
                }
            }

            // Changing the edit mode should occur after changing the CurrentItem
            this.EditMode = EditMode.None;

            this.isRollingBackChanges = false;
        }

        /// <summary>
        /// Called when edit mode changed.
        /// </summary>
        private void OnEditModeChanged()
        {
            EventHandler handler = this.EditModeChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
