#region License
// ValidationHelper.cs
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
    using ViewModels;

    /// <summary>
    /// Helps validation for MVVM pattern.
    /// </summary>
    public class ValidationHelper : DependencyObject
    {
        /// <summary>
        /// Identifies the <see cref="ValidationController" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValidationControllerProperty = DependencyProperty.RegisterAttached(
            "ValidationController",
            typeof(IValidationController),
            typeof(ValidationHelper),
            new UIPropertyMetadata(null, OnValidationControllerChanged));

        /// <summary>
        /// Gets the validation controller.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static IValidationController GetValidationController(DependencyObject obj)
        {
            return (IValidationController)obj.GetValue(ValidationControllerProperty);
        }

        /// <summary>
        /// Sets the validation controller.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <param name="value"> The value. </param>
        public static void SetValidationController(DependencyObject obj, IValidationController value)
        {
            obj.SetValue(ValidationControllerProperty, value);
        }

        /// <summary>
        /// Called when validation controller changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnValidationControllerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            ((IValidationController)e.NewValue).Attach(sender);
        }
    }
}
