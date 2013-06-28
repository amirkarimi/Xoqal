#region License
// UserExtensions.cs
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// <see cref="IUser"/> extensions.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Determines whether the user has the specified roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>
        /// <c>true</c> if the user has the specified roles; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRole(this IUser user, params string[] roles)
        {
            return user.Roles.Any(r => roles.Contains(r.Name));
        }

        /// <summary>
        /// Determines whether the user has the specified permissions.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permissionItems">The permission items.</param>
        /// <returns>
        ///   <c>true</c> if the user has the specified permissions; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInPermission(this IUser user, params PermissionItem[] permissionItems)
        {
            return UserExtensions.IsInPermission(user, permissionItems.Select(p => p.PermissionId).ToArray());
        }

        /// <summary>
        /// Determines whether the user has the specified permissions.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permissions">The permissions.</param>
        /// <returns>
        /// <c>true</c> if the user has the specified permissions; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInPermission(this IUser user, params string[] permissions)
        {
            return user.Roles.Any(r => r.RolesInPermissions.Any(p => permissions.Contains(p.PermissionId)));
        }

        /// <summary>
        /// Hashes and sets the specified password for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        public static void SetHashedPassword(this IUser user, string password)
        {
            user.Password = PasswordHasherFactory.Default.Hash(password);
        }
    }
}
