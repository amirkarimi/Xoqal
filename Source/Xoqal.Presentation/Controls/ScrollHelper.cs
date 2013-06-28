#region License
// ScrollHelper.cs
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
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Helps to manage scrolling in MVVM ways.
    /// </summary>
    public class ScrollHelper : DependencyObject
    {
        /// <summary>
        /// Identifies the IsAutoScrollToEnd dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAutoScrollToEndProperty = DependencyProperty.RegisterAttached(
            "IsAutoScrollToEnd", typeof(bool), typeof(ScrollHelper), new UIPropertyMetadata(false, OnIsAutoScrollToEndChanged));

        /// <summary>
        /// Gets the is auto scroll to end.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <returns> </returns>
        public static bool GetIsAutoScrollToEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAutoScrollToEndProperty);
        }

        /// <summary>
        /// Sets the is auto scroll to end.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <param name="value"> The value. </param>
        public static void SetIsAutoScrollToEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutoScrollToEndProperty, value);
        }

        /// <summary>
        /// Called when IsAutoScrollToEndChanged changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnIsAutoScrollToEndChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBoxBase = sender as TextBoxBase;
            if (textBoxBase != null)
            {
                textBoxBase.TextChanged += OnTextBoxBaseTextChanged;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBoxBase control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.Windows.Controls.TextChangedEventArgs" /> instance containing the event data. </param>
        private static void OnTextBoxBaseTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBoxBase)sender).ScrollToEnd();
        }
    }
}
