#region License
// Pager.cs
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
    using ViewModels;

    /// <summary>
    /// Pager control which is integrated with <see cref="Xoqal.Presentation.ViewModels.PagerController" /> .
    /// </summary>
    public class Pager : Control
    {
        /// <summary>
        /// Identifies the <see cref="PagerController" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PagerControllerProperty = DependencyProperty.Register(
            "PagerController", typeof(IPagerController), typeof(Pager), new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes the <see cref="Pager" /> class.
        /// </summary>
        static Pager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager), new FrameworkPropertyMetadata(typeof(Pager)));
        }

        /// <summary>
        /// Gets or sets the page controller.
        /// </summary>
        /// <value> The page controller. </value>
        public IPagerController PagerController
        {
            get { return (PagerController)this.GetValue(PagerControllerProperty); }
            set { this.SetValue(PagerControllerProperty, value); }
        }
    }
}
