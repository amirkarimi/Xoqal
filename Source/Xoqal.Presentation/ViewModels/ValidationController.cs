#region License
// ValidationController.cs
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
    using System.Windows;
    using System.Windows.Controls;
    using Xoqal.Core.Models;

    /// <summary>
    /// Default implementation of the <see cref="IValidationController" />
    /// </summary>
    public class ValidationController : NotificationObject, IValidationController
    {
        private int errorCount;
        private bool hasError;
        private bool isAttached;

        #region IValidationController Members

        /// <summary>
        /// Gets a value indicating whether the attached element has any validation error.
        /// </summary>
        /// <value> <c>true</c> if the attached element has any validation error; otherwise, <c>false</c> . </value>
        public bool HasError
        {
            get
            {
                return this.hasError;
            }

            private set
            {
                if (this.hasError == value)
                {
                    return;
                }

                this.hasError = value;
                this.RaisePropertyChanged(() => this.HasError);
            }
        }

        /// <summary>
        /// Gets a value indicating whether validation controller is attached to the corresponding element.
        /// </summary>
        /// <value> <c>true</c> if validation controller is attached to the corresponding element; otherwise, <c>false</c> . </value>
        public bool IsAttached
        {
            get
            {
                return this.isAttached;
            }

            private set
            {
                if (this.isAttached == value)
                {
                    return;
                }

                this.isAttached = value;
                this.RaisePropertyChanged(() => this.IsAttached);
            }
        }

        /// <summary>
        /// Attaches to the element for validation control.
        /// </summary>
        /// <param name="element"> </param>
        public void Attach(DependencyObject element)
        {
            Validation.AddErrorHandler(element, this.OnValidationError);
            this.IsAttached = true;
        }

        #endregion

        /// <summary>
        /// Called when validation error occurred.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.Controls.ValidationErrorEventArgs" /> instance containing the event data. </param>
        private void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            switch (e.Action)
            {
                case ValidationErrorEventAction.Added:
                    this.errorCount++;
                    break;
                case ValidationErrorEventAction.Removed:
                    if (this.errorCount > 0)
                    {
                        this.errorCount--;
                    }

                    break;
            }

            this.HasError = this.errorCount > 0;
        }
    }
}
