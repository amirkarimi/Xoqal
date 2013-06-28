#region License
// ICrudViewModel.cs
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
    /// Represents the base functionality for a simple view-model CRUD operation.
    /// </summary>
    public interface ICrudViewModel : IPresenterViewModel
    {
        /// <summary>
        /// Occurs when edit mode changed.
        /// </summary>
        event EventHandler EditModeChanged;

        /// <summary>
        /// Gets the add command.
        /// </summary>
        ICommand AddCommand { get; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        ICommand EditCommand { get; }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        ICommand DeleteCommand { get; }

        /// <summary>
        /// Gets the accept changes command.
        /// </summary>
        ICommand AcceptChangesCommand { get; }

        /// <summary>
        /// Gets the cancel changes command.
        /// </summary>
        ICommand CancelChangesCommand { get; }

        /// <summary>
        /// Gets or sets the edit mode.
        /// </summary>
        /// <value> The edit mode. </value>
        EditMode EditMode { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is in edit mode.
        /// </summary>
        /// <value> <c>true</c> if this instance is in edit mode; otherwise, <c>false</c> . </value>
        bool IsInEditMode { get; }

        /// <summary>
        /// Gets or sets the validation controller.
        /// </summary>
        /// <value> The validation controller. </value>
        IValidationController ValidationController { get; set; }
    }
}
