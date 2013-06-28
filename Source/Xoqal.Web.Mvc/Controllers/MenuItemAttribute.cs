#region License
// MenuItemAttribute.cs
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

namespace Xoqal.Web.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Indicates that a controller action could be presented as a menu item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class MenuItemAttribute : Attribute
    {
        private string title;
        private string category;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemAttribute"/> class.
        /// </summary>
        public MenuItemAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the resource type.
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the resource name for title.
        /// </summary>
        public string TitleResourceName { get; set; }

        /// <summary>
        /// Gets or sets the resource name for category name.
        /// </summary>
        public string CategoryResourceName { get; set; }

        /// <summary>
        /// Gets or sets the action method name.
        /// </summary>
        /// <remarks>
        /// It will be ignored if the <see cref="MenuItemAttribute"/> target is a method.
        /// </remarks>
        public string ActionMethodName { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this.title))
                {
                    this.title = Utilities.ResourceHelper.GetResourceValue(this.ResourceType, this.TitleResourceName);
                }

                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        public string Category
        {
            get
            {
                if (string.IsNullOrEmpty(this.category))
                {
                    this.category = Utilities.ResourceHelper.GetResourceValue(this.ResourceType, this.CategoryResourceName);
                }

                return this.category;
            }

            set
            {
                this.category = value;
            }
        }
    }
}
