#region License
// SimpleDataGrid.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;

    /// <summary>
    /// Simple in memory data grid.
    /// </summary>
    public class SimpleDataGrid : DataGrid
    {
        /// <summary>
        /// Identifies the <see cref="PagerController" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PagerControllerProperty = DependencyProperty.Register(
            "PagerController", typeof(PagerController), typeof(SimpleDataGrid), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SelectedDataItem" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedDataItemProperty = DependencyProperty.Register(
            "SelectedDataItem",
            typeof(object),
            typeof(SimpleDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDataItemChanged));

        /// <summary>
        /// Identifies the <see cref="SelectedItems" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems",
            typeof(IList),
            typeof(SimpleDataGrid),
            new FrameworkPropertyMetadata(new List<object>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Initializes the <see cref="SimpleDataGrid" /> class.
        /// </summary>
        static SimpleDataGrid()
        {
            IsEnabledProperty.OverrideMetadata(typeof(SimpleDataGrid), new FrameworkPropertyMetadata(OnIsEnabledChanged));
            SelectedItemProperty.OverrideMetadata(typeof(SimpleDataGrid), new FrameworkPropertyMetadata(OnSelectedItemChanged));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDataGrid" /> class.
        /// </summary>
        public SimpleDataGrid()
        {
            this.LastSelectedIndex = -1;
        }

        /// <summary>
        /// Gets or sets the pager controller.
        /// </summary>
        /// <value> The pager controller. </value>
        public PagerController PagerController
        {
            get { return (PagerController)this.GetValue(PagerControllerProperty); }
            set { this.SetValue(PagerControllerProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected data.
        /// </summary>
        /// <value> The selected data. </value>
        public object SelectedDataItem
        {
            get { return this.GetValue(SelectedDataItemProperty); }
            set { this.SetValue(SelectedDataItemProperty, value); }
        }

        /// <summary>
        /// Gets the items in the <see cref="T:System.Windows.Controls.Primitives.MultiSelector" /> that are selected.
        /// </summary>
        /// <returns> The items in the <see cref="T:System.Windows.Controls.Primitives.MultiSelector" /> that are selected. </returns>
        public new IList SelectedItems
        {
            get { return (ObservableCollection<object>)this.GetValue(SelectedItemsProperty); }
            set { this.SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected index before last change.
        /// </summary>
        protected int LastSelectedIndex { get; set; }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Controls.DataGrid.LoadingRow" /> event.
        /// </summary>
        /// <param name="e"> The data for the event. </param>
        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            if (this.PagerController != null)
            {
                e.Row.Header =
                    (e.Row.GetIndex() + 1 +
                        (this.PagerController.PageSize * ((this.PagerController.CurrentPage < 1 ? 1 : this.PagerController.CurrentPage) - 1)))
                        .ToString();
            }
            else
            {
                e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            }
        }

        /// <summary>
        /// Invoked when the selection changes.
        /// </summary>
        /// <param name="e"> The data for the event. </param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            this.SelectedItems = base.SelectedItems;
            /*
            if (this.SelectedItems == null)
            {
                return;
            }

            if (e.AddedItems != null)
            {
                foreach (var item in e.AddedItems)
                {
                    this.SelectedItems.Add(item);
                }
            }

            if (e.RemovedItems != null)
            {
                foreach (var item in e.RemovedItems)
                {
                    this.SelectedItems.Remove(item);
                }
            }*/
        }

        /// <summary>
        /// Called when IsEnabled changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnIsEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = (SimpleDataGrid)sender;
            if ((bool)e.NewValue)
            {
                if (grid.LastSelectedIndex != -1)
                {
                    grid.SelectedIndex = grid.LastSelectedIndex;
                }
            }
            else
            {
                grid.LastSelectedIndex = grid.SelectedIndex;
            }
        }

        /// <summary>
        /// Called when SelectedDataItem changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnSelectedDataItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SimpleDataGrid)sender).SelectedItem = e.NewValue;
        }

        /// <summary>
        /// Called when SelectedItem changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var simpleDataGrid = (SimpleDataGrid)sender;
            if (simpleDataGrid.IsEnabled)
            {
                simpleDataGrid.SelectedDataItem = e.NewValue;
            }
        }
    }
}
