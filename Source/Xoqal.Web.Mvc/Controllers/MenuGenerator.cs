#region License
// MenuGenerator.cs
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
    using System.Reflection;
    using System.Text;
    using System.Web.Mvc;
    using Xoqal.Web.Mvc.Security;

    /// <summary>
    /// Generates the base structure used to represents a menu according to the <see cref="MenuItemAttribute"/> 
    /// attributes specified for the controllers.
    /// </summary>
    public class MenuGenerator
    {
        private const string ControllerSuffix = "Controller";

        /// <summary>
        /// Gets the menu items from the specified assembly matching the specified namespace.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="namespaceFilter"></param>
        /// <returns></returns>
        public IEnumerable<Models.MenuItem> GetMenuItems(Assembly assembly, string namespaceFilter = null)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (string.IsNullOrWhiteSpace(namespaceFilter) || type.Namespace == namespaceFilter)
                {
                    var typeMenuItemAttribute = type.GetCustomAttributes(true).OfType<MenuItemAttribute>().FirstOrDefault();
                    var typePermissionAttribute = type.GetCustomAttributes(true).OfType<PermissionAttribute>().FirstOrDefault();

                    // Process type
                    if (typeMenuItemAttribute != null)
                    {
                        var menuItem = this.GetTypeMenuItem(type, typeMenuItemAttribute, typePermissionAttribute);
                        if (menuItem != null)
                        {
                            yield return menuItem;
                        }
                    }

                    // Process methods
                    var methodInformations = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                    foreach (var methodInfo in methodInformations)
                    {
                        var methodMenuItemAttribute = methodInfo.GetCustomAttributes(true).OfType<MenuItemAttribute>().FirstOrDefault();
                        if (methodMenuItemAttribute != null)
                        {
                            var menuItem = this.GetMethodMenuItem(type, methodInfo, typePermissionAttribute, typeMenuItemAttribute, methodMenuItemAttribute);
                            if (menuItem != null)
                            {
                                yield return menuItem;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the menu item of the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="menuItemAttribute"></param>
        /// <param name="typePermissionAttribute"></param>
        /// <returns></returns>
        private Models.MenuItem GetTypeMenuItem(
            Type type, 
            MenuItemAttribute menuItemAttribute,
            PermissionAttribute typePermissionAttribute)
        {
            if (string.IsNullOrWhiteSpace(menuItemAttribute.ActionMethodName))
            {
                throw new InvalidOperationException("You should set the ActionMethodName property of the MenuItemAttribute when its target is a class");
            }

            var methodInfo = type.GetMethod(menuItemAttribute.ActionMethodName);
            var methodMenuItemAttribute = methodInfo.GetCustomAttributes(true).OfType<MenuItemAttribute>().FirstOrDefault();
            return this.GetMethodMenuItem(type, methodInfo, typePermissionAttribute, menuItemAttribute, methodMenuItemAttribute);
        }

        /// <summary>
        /// Gets the menu item of the specified method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="typePermissionAttribute">The type permission attribute.</param>
        /// <param name="typeMenuItemAttribute">The type menu item attribute.</param>
        /// <param name="methodMenuItemAttribute">The method menu item attribute.</param>
        /// <returns></returns>
        private Models.MenuItem GetMethodMenuItem(
            Type type,
            MethodInfo methodInfo, 
            PermissionAttribute typePermissionAttribute, 
            MenuItemAttribute typeMenuItemAttribute,
            MenuItemAttribute methodMenuItemAttribute)
        {
            var methodPermissionAttribute = methodInfo.GetCustomAttributes(true).OfType<PermissionAttribute>().FirstOrDefault();
            var methodAllowAnonymousAttribute = methodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().FirstOrDefault();
            var methodActionNameAttribute = methodInfo.GetCustomAttributes(true).OfType<ActionNameAttribute>().FirstOrDefault();

            if (typeMenuItemAttribute == null && methodMenuItemAttribute == null)
            {
                return null;
            }

            PermissionAttribute permissionAttribute = null;

            if (typePermissionAttribute != null && methodAllowAnonymousAttribute == null)
            {
                permissionAttribute = typePermissionAttribute;
            }

            if (methodPermissionAttribute != null)
            {
                permissionAttribute = methodPermissionAttribute;
            }

            var actionName = methodActionNameAttribute == null ?
                methodInfo.Name :
                methodActionNameAttribute.Name;

            var controllerName = type.Name;
            if (controllerName.EndsWith(ControllerSuffix))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - ControllerSuffix.Length);
            }

            var menuItemAttribute = methodMenuItemAttribute ?? typeMenuItemAttribute;

            return new Models.MenuItem(
                menuItemAttribute.Order,
                menuItemAttribute.Title,
                menuItemAttribute.Category,
                actionName,
                controllerName,
                permissionAttribute,
                null);
        }
    }
}
