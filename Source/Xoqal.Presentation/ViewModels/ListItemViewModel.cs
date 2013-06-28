#region License
// ListItemViewModel.cs
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
    using Xoqal.Core.Models;

    /// <summary>
    /// Represents a list item view-model.
    /// </summary>
    public class ListItemViewModel : NotificationObject
    {
        private object data;
        private bool isSelected;
        private string text;

        /// <summary>
        /// Occurs when IsSelected changed.
        /// </summary>
        public event EventHandler IsSelectedChanged;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value> <c>true</c> if this instance is selected; otherwise, <c>false</c> . </value>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected == value)
                {
                    return;
                }

                this.isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
                this.OnIsSelectedChanged();
            }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value> The data. </value>
        public object Data
        {
            get
            {
                return this.data;
            }

            set
            {
                if (this.data == value)
                {
                    return;
                }

                this.data = value;
                this.RaisePropertyChanged(() => this.Data);
            }
        }

        /// <summary>
        /// Gets or sets the text which presents the text value of the current item.
        /// </summary>
        /// <value> The text value of the current item. </value>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.RaisePropertyChanged(() => this.Text);
            }
        }

        /// <summary>
        /// Called when IsSelected changed.
        /// </summary>
        protected virtual void OnIsSelectedChanged()
        {
            EventHandler handler = this.IsSelectedChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
