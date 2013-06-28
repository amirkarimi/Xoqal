#region License
// EnumRadioList.cs
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
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;
    using Xoqal.Utilities;

    /// <summary>
    /// Represents a single selection radio button list.
    /// </summary>
    public class EnumRadioList : ListBox
    {
        /// <summary>
        /// Identifies the <see cref="EnumType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
            "EnumType", typeof(Type), typeof(EnumRadioList), new UIPropertyMetadata(null, OnEnumTypeChanged));

        /// <summary>
        /// Identifies the <see cref="ExcludedValues" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExcludedValuesProperty =
            DependencyProperty.Register("ExcludedValues", typeof(object[]), typeof(EnumRadioList), new PropertyMetadata(null, OnEnumTypeChanged));

        /// <summary>
        /// Initializes the <see cref="EnumRadioList" /> class.
        /// </summary>
        static EnumRadioList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumRadioList), new FrameworkPropertyMetadata(typeof(EnumRadioList)));
        }

        /// <summary>
        /// Gets or sets the type of the enum.
        /// </summary>
        /// <value> The type of the enum. </value>
        public Type EnumType
        {
            get { return (Type)this.GetValue(EnumTypeProperty); }
            set { this.SetValue(EnumTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the excluded values.
        /// </summary>
        /// <value>
        /// The excluded values.
        /// </value>
        public object[] ExcludedValues
        {
            get { return (object[])GetValue(ExcludedValuesProperty); }
            set { this.SetValue(ExcludedValuesProperty, value); }
        }

        /// <summary>
        /// Called when EnumType changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnEnumTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnumRadioList)sender;

            if (control.EnumType != null)
            {
                var enums = EnumHelper.GetEnumValueDisplays(control.EnumType)
                    .Select(i => new EnumCheckBoxItem { Value = i.Key, Text = i.Value }).ToList();

                if (control.ExcludedValues != null && control.ExcludedValues.Count() != 0)
                {
                    enums = enums.Where(i => !control.ExcludedValues.Contains(i.Value)).ToList();
                }

                control.ItemsSource = enums;
            }
        }
    }
}
