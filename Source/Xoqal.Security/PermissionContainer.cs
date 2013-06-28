#region License
// PermissionContainer.cs
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

namespace Xoqal.Security
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents search and load functionality to find all permission which defined in the driven projects.
    /// </summary>
    public class PermissionContainer
    {
        private static readonly object SyncObject = new object();
        private static List<PermissionItem> permissions;

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        public static List<PermissionItem> Permissions
        {
            get
            {
                // Checking out of lock block to increase performance
                if (permissions != null)
                {
                    return permissions;
                }

                lock (SyncObject)
                {
                    if (permissions == null)
                    {
                        LoadPermissions();
                    }

                    return permissions;
                }
            }
        }

        /// <summary>
        /// Loads the permissions.
        /// </summary>
        private static void LoadPermissions()
        {
            // Search among the assemblies which defined the SecurityRegulatorAttribute.
            IEnumerable<Assembly> assemblies =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDefined(typeof(SecurityRegulatorAttribute), false));

            // Create new permission list
            permissions = new List<PermissionItem>();

            // Find PermissionContainerType in the found assemblies.
            foreach (Assembly assembly in assemblies)
            {
                var attribute =
                    (SecurityRegulatorAttribute)assembly.GetCustomAttributes(typeof(SecurityRegulatorAttribute), false).FirstOrDefault();

                if (attribute != null)
                {
                    FieldInfo[] fields = attribute.PermissionContainerType.GetFields(BindingFlags.Public | BindingFlags.Static);

                    List<PermissionItem> permissionItems =
                        fields.Where(p => p.FieldType == typeof(PermissionItem)).Select(p => (PermissionItem)p.GetValue(null)).ToList();

                    // Add all found permissions to permission list
                    permissions.AddRange(permissionItems);
                }
            }

            Debug.WriteIf(permissions.Count == 0, "PermissionContainer.LoadPermissions called but no permission found.");
        }
    }
}
