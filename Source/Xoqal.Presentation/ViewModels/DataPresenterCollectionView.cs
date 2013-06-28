#region License
// DataPresenterCollectionView.cs
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows.Data;

    public class DataPresenterCollectionView : ListCollectionView
    {
        private SortDescriptionCollection sortDescriptionCollection;
        private bool needsRefresh = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPresenterCollectionView" /> class.
        /// </summary>
        /// <param name="list">The underlying collection, which must implement <see cref="T:System.Collections.IList" />.</param>
        public DataPresenterCollectionView(IList list)
            : base(list)
        {
        }

        /// <summary>
        /// Occurs when the collection refreshed.
        /// </summary>
        public event EventHandler<EventArgs> Refreshed;

        /// <summary>
        /// Gets a value indicating whether needs refresh.
        /// </summary>
        /// <value>
        /// <c>true</c> if needs refresh; otherwise, <c>false</c>.
        /// </value>
        public override bool NeedsRefresh
        {
            get { return base.NeedsRefresh || this.needsRefresh; }
        }

        /// <summary>
        /// Gets the sort descriptions.
        /// </summary>
        /// <value>
        /// The sort descriptions.
        /// </value>
        public override SortDescriptionCollection SortDescriptions
        {
            get
            {
                if (this.sortDescriptionCollection == null)
                {
                    this.sortDescriptionCollection = new SortDescriptionCollection();
                    ((INotifyCollectionChanged)this.sortDescriptionCollection).CollectionChanged += this.OnSortDescriptionCollectionChanged;
                }

                return this.sortDescriptionCollection;
            }
        }

        /// <summary>
        /// Re-creates the view.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            this.OnRefreshed();
        }

        /// <summary>
        /// Called when this collection refreshed.
        /// </summary>
        protected virtual void OnRefreshed()
        {
            this.needsRefresh = false;

            var handler = this.Refreshed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when sort description collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void OnSortDescriptionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.needsRefresh = true;
        }
    }
}
