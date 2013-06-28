#region License
// SecurityHelper.cs
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

namespace Xoqal.Presentation.Security
{
    using System;
    using System.Linq;
    using System.Windows;
    using Xoqal.Presentation.Utilities;
    using Xoqal.Security;

    /// <summary>
    /// Helps control the security over UI.
    /// </summary>
    public class SecurityHelper : DependencyObject
    {
        /// <summary>
        /// Identifies the Permission dependency property
        /// </summary>
        public static readonly DependencyProperty PermissionProperty = DependencyProperty.RegisterAttached(
            "Permission", typeof(string), typeof(SecurityHelper), new PropertyMetadata(null, OnPermissionChange));

        /// <summary>
        /// Identifies the <see cref="Visibility" /> dependency property
        /// </summary>
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached(
            "Visibility", typeof(Visibility), typeof(SecurityHelper), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <returns> </returns>
        public static string GetPermission(DependencyObject obj)
        {
            return (string)obj.GetValue(PermissionProperty);
        }

        /// <summary>
        /// Sets the permission.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <param name="value"> The value. </param>
        public static void SetPermission(DependencyObject obj, string value)
        {
            obj.SetValue(PermissionProperty, value);
        }

        /// <summary>
        /// Gets the visibility.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <returns> </returns>
        public static Visibility GetVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(VisibilityProperty);
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <param name="value"> The value. </param>
        public static void SetVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(VisibilityProperty, value);
        }

        /// <summary>
        /// Called when Permission change.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data. </param>
        private static void OnPermissionChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Loaded += OnFrameworkElementLoaded;
            }
        }

        /// <summary>
        /// Handles the Loaded event of the FrameworkElement control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data. </param>
        private static void OnFrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            if (!DesignTime.IsInDesignMode)
            {
                var element = (FrameworkElement)sender;
                string permission = GetPermission(element);
                if (!string.IsNullOrEmpty(permission))
                {
                    var user = Authentication.Default.GetCurrentUserPrincipal();
                    string[] permissions = permission.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                    if (user == null || !permissions.Any(p => user.IsInPermission(p)))
                    {
                        element.Visibility = GetVisibility(element);
                    }
                }
            }
        }
    }
}
