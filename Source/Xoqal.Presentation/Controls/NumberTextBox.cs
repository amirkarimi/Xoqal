#region License
// NumberTextBox.cs
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

namespace Xoqal.Presentation.Controls
{
    using System.Linq;
    using System.Media;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Represents a numeric text box.
    /// </summary>
    public class NumericTextBox : TextBox
    {
        /// <summary>
        /// Identifies the <see cref="CanAcceptDecimalPlace" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CanAcceptDecimalPlaceProperty = DependencyProperty.Register(
            "CanAcceptDecimalPlace", typeof(bool), typeof(NumericTextBox), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether this instance can accept decimal place.
        /// </summary>
        /// <value> <c>true</c> if this instance can accept decimal place; otherwise, <c>false</c> . </value>
        public bool CanAcceptDecimalPlace
        {
            get { return (bool)this.GetValue(CanAcceptDecimalPlaceProperty); }
            set { this.SetValue(CanAcceptDecimalPlaceProperty, value); }
        }

        /// <summary>
        /// Called when the <see cref="E:System.Windows.UIElement.KeyDown" /> occurs.
        /// </summary>
        /// <param name="e"> The event data. </param>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // We dont want space key. So put it here not in the KeyDown event handler.
            e.Handled = e.Key == Key.Space;
            base.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="System.Windows.Input.KeyEventArgs" /> attached routed event reaches an element derived from this class in its route. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e"> Provides data about the event. </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var decimalKeys = new[] { Key.Decimal, Key.OemPeriod };

            e.Handled =
                !(e.Key == Key.Tab || (this.CanAcceptDecimalPlace && !this.Text.Contains('.') && decimalKeys.Contains(e.Key)) ||
                    (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9));

            if (e.Handled)
            {
                SystemSounds.Beep.Play();
            }

            base.OnKeyDown(e);
        }
    }
}
