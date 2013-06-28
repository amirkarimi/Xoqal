#region License
// MultiSelectorListBox.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;

    /// <summary>
    /// Represents a list box with multi selection ability.
    /// </summary>
    public class MultiSelectorListBox : ItemsControl
    {
        /// <summary>
        /// Identifies the <see cref="SelectedItems" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiSelectorListBox), new UIPropertyMetadata(null, OnSelectedItemsChanged));

        /// <summary>
        /// Identifies the <see cref="IsAllChecked" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAllCheckedProperty =
            DependencyProperty.Register("IsAllChecked", typeof(bool), typeof(MultiSelectorListBox), new PropertyMetadata(false, OnIsAllCheckedChanged));

        /// <summary>
        /// Initializes the <see cref="MultiSelectorListBox" /> class.
        /// </summary>
        static MultiSelectorListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MultiSelectorListBox), new FrameworkPropertyMetadata(typeof(MultiSelectorListBox)));
        }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value> The selected items. </value>
        public IList SelectedItems
        {
            get { return (IList)this.GetValue(SelectedItemsProperty); }
            set { this.SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is all checked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is all checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllChecked
        {
            get { return (bool)GetValue(IsAllCheckedProperty); }
            set { this.SetValue(IsAllCheckedProperty, value); }
        }

        /// <summary>
        /// Called when the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource" /> property changes.
        /// </summary>
        /// <param name="oldValue"> Old value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource" /> property. </param>
        /// <param name="newValue"> New value of the <see cref="P:System.Windows.Controls.ItemsControl.ItemsSource" /> property. </param>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            var previousItemsSource = oldValue as ICollection<ListItemViewModel>;
            if (previousItemsSource != null)
            {
                foreach (ListItemViewModel item in previousItemsSource)
                {
                    item.IsSelectedChanged -= this.OnItemIsSelectedChanged;
                }
            }

            var itemsSource = newValue as ICollection<ListItemViewModel>;
            if (itemsSource != null)
            {
                foreach (ListItemViewModel item in itemsSource)
                {
                    item.IsSelectedChanged += this.OnItemIsSelectedChanged;
                }

                foreach (ListItemViewModel item in itemsSource)
                {
                    item.IsSelected = this.SelectedItems != null && this.SelectedItems.Contains(item.Data);
                }
            }

            this.IsAllChecked = false;
        }

        /// <summary>
        /// Called when SelectedItems changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnSelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var multiSelectorListBox = (MultiSelectorListBox)sender;
            var selectedItems = (IList)e.NewValue;

            if (multiSelectorListBox.ItemsSource == null)
            {
                return;
            }

            foreach (ListItemViewModel item in (ICollection<ListItemViewModel>)multiSelectorListBox.ItemsSource)
            {
                item.IsSelected = selectedItems != null && selectedItems.Contains(item.Data);
            }
        }

        /// <summary>
        /// Called when is all checked changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnIsAllCheckedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var multiSelectorListBox = (MultiSelectorListBox)sender;
            var items = (ICollection<ListItemViewModel>)multiSelectorListBox.ItemsSource;
            if (items == null || items.Count == 0)
            {
                return;
            }

            foreach (ListItemViewModel item in items)
            {
                item.IsSelected = (bool)e.NewValue;
            }
        }

        /// <summary>
        /// Handles the IsSelectedChanged event of the Item control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.EventArgs" /> instance containing the event data. </param>
        private void OnItemIsSelectedChanged(object sender, EventArgs e)
        {
            if (this.SelectedItems == null)
            {
                return;
            }

            var item = (ListItemViewModel)sender;
            if (item.IsSelected)
            {
                if (!this.SelectedItems.Contains(item.Data))
                {
                    this.SelectedItems.Add(item.Data);
                }
            }
            else
            {
                if (this.SelectedItems.Contains(item.Data))
                {
                    this.SelectedItems.Remove(item.Data);
                }
            }
        }
    }
}
