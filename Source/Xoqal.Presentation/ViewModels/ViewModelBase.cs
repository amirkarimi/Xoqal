#region License
// ViewModelBase.cs
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
    using Xoqal.Core.Models;

    /// <summary>
    /// Represents the base class for standard View-Models.
    /// </summary>
    public abstract class ViewModelBase : NotificationObject, IViewModel
    {
        private bool isEnabled = true;

        #region IViewModel Members

        /// <summary>
        /// Gets the view title.
        /// </summary>
        /// <value> The view title. </value>
        public virtual string ViewTitle
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this view-model is enabled.
        /// </summary>
        /// <value> <c>true</c> if this view-mode is enabled; otherwise, <c>false</c> . </value>
        public virtual bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                if (this.isEnabled == value)
                {
                    return;
                }

                this.isEnabled = value;
                this.RaisePropertyChanged(() => this.IsEnabled);
            }
        }

        #endregion
    }
}
