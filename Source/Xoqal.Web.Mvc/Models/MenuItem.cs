#region License
// MenuItem.cs
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

namespace Xoqal.Web.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xoqal.Web.Mvc.Security;

    /// <summary>
    /// Represents a menu item.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem" /> class.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="title">The title.</param>
        /// <param name="category">The category.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="permissionAttribute">The permission attribute.</param>
        /// <param name="routeValues">The route values.</param>
        public MenuItem(
            int order,
            string title,
            string category,
            string actionName,
            string controllerName,
            PermissionAttribute permissionAttribute,
            object routeValues)
        {
            this.Order = order;
            this.Title = title;
            this.Category = category;
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.PermissionAttribute = permissionAttribute;
            this.RouteValues = routeValues;
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the action name.
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the permission attribute.
        /// </summary>
        public PermissionAttribute PermissionAttribute { get; private set; }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        public object RouteValues { get; private set; }
    }
}
