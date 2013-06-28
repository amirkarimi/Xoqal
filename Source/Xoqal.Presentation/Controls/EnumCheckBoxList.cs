#region License
// EnumCheckBoxList.cs
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;
    using Xoqal.Utilities;

    /// <summary>
    /// Represents a multiple selection enum control with support of the Flags.
    /// </summary>
    public class EnumCheckBoxList : Control
    {
        /// <summary>
        /// Identifies the <see cref="EnumFlagsValue" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumFlagsValueProperty = DependencyProperty.Register(
            "EnumFlagsValue",
            typeof(object),
            typeof(EnumCheckBoxList),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnEnumFlagsValueChanged));

        /// <summary>
        /// Identifies the <see cref="SelectedValues" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedValuesProperty = DependencyProperty.Register(
            "SelectedValues",
            typeof(ObservableCollection<object>),
            typeof(EnumCheckBoxList),
            new UIPropertyMetadata(new ObservableCollection<object>()));

        /// <summary>
        /// Identifies the <see cref="EnumType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
            "EnumType", typeof(Type), typeof(EnumCheckBoxList), new UIPropertyMetadata(null, OnEnumTypeChanged));

        /// <summary>
        /// Identifies the <see cref="EnumItems" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnumItemsProperty = DependencyProperty.Register(
            "EnumItems", typeof(IList<EnumCheckBoxItem>), typeof(EnumCheckBoxList), new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes the <see cref="EnumCheckBoxList" /> class.
        /// </summary>
        static EnumCheckBoxList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumCheckBoxList), new FrameworkPropertyMetadata(typeof(EnumCheckBoxList)));
        }

        /// <summary>
        /// Gets or sets the enum flags value.
        /// </summary>
        /// <value>
        /// The enum flags value.
        /// </value>
        public object EnumFlagsValue
        {
            get { return this.GetValue(EnumFlagsValueProperty); }
            set { this.SetValue(EnumFlagsValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected values.
        /// </summary>
        /// <value> The selected values. </value>
        public ObservableCollection<object> SelectedValues
        {
            get { return (ObservableCollection<object>)this.GetValue(SelectedValuesProperty); }
            set { this.SetValue(SelectedValuesProperty, value); }
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
        /// Gets or sets the enum items.
        /// </summary>
        /// <value> The enum items. </value>
        private IList<EnumCheckBoxItem> EnumItems
        {
            get { return (IList<EnumCheckBoxItem>)this.GetValue(EnumItemsProperty); }
            set { this.SetValue(EnumItemsProperty, value); }
        }

        /// <summary>
        /// Called when EnumType changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnEnumTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnumCheckBoxList)sender;

            if (control.EnumType != null)
            {
                control.EnumItems =
                    EnumHelper.GetEnumObjectDisplays(control.EnumType)
                        .Select(i => 
                            new EnumCheckBoxItem(control.OnSelectionChanged)
                            {
                                Value = i.Key, 
                                Text = i.Value, 
                                IsSelected = false
                            })
                        .ToList();
            }

            control.SelectedValues.Clear();
        }

        /// <summary>
        /// Called when EnumFlagsValue changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnEnumFlagsValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnumCheckBoxList)sender;

            var selectedValues = new ObservableCollection<object>();
            int flagsValue = Convert.ToInt32(control.EnumFlagsValue);
            foreach (EnumCheckBoxItem item in control.EnumItems)
            {
                int logicalValue = Convert.ToInt32(item.Value);
                item.SetSilentIsSelected(flagsValue != 0 && (flagsValue & logicalValue) != 0);
                if (item.IsSelected)
                {
                    selectedValues.Add(item.Value);
                }
            }

            control.SelectedValues = selectedValues;
        }

        /// <summary>
        /// Called when selection of an item changed.
        /// </summary>
        /// <param name="item"> The item. </param>
        private void OnSelectionChanged(EnumCheckBoxItem item)
        {
            if (item.IsSelected)
            {
                this.SelectedValues.Add(item.Value);
            }
            else
            {
                this.SelectedValues.Remove(item.Value);
            }

            int flagsValue = 0;
            foreach (object value in this.SelectedValues)
            {
                flagsValue |= Convert.ToInt32(value);
            }

            this.EnumFlagsValue = flagsValue;
        }
    }
}
