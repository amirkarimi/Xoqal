#region License
// EnumCheckBoxItem.cs
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
    /// Represents an item of the EnumCheckBoxList control.
    /// </summary>
    public class EnumCheckBoxItem : NotificationObject
    {
        private readonly Action<EnumCheckBoxItem> isSelectedChangedCallback;
        private bool isSelected;
        private string text;
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumCheckBoxItem" /> class.
        /// </summary>
        /// <param name="isSelectedChangedCallback"> The is selected changed callback. </param>
        public EnumCheckBoxItem(Action<EnumCheckBoxItem> isSelectedChangedCallback = null)
        {
            this.isSelectedChangedCallback = isSelectedChangedCallback;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value> The value. </value>
        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                this.RaisePropertyChanged(() => this.Value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value> The text. </value>
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

                if (this.isSelectedChangedCallback != null)
                {
                    this.isSelectedChangedCallback(this);
                }
            }
        }

        /// <summary>
        /// Sets the IsSelected property silently.
        /// </summary>
        /// <param name="isSelected"> if set to <c>true</c> [is selected]. </param>
        public void SetSilentIsSelected(bool isSelected)
        {
            this.isSelected = isSelected;
            this.RaisePropertyChanged(() => this.IsSelected);
        }
    }
}
