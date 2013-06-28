#region License
// UserPrincipal.cs
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
    using System.Linq;
    using System.Security.Principal;

    /// <summary>
    /// Represents a user principal.
    /// </summary>
    public class UserPrincipal : IUserPrincipal
    {
        private readonly string[] roles;
        private readonly string[] permissions;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPrincipal" /> class.
        /// </summary>
        /// <param name="identity">The user.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="permissions">The permissions.</param>
        public UserPrincipal(IUserIdentity identity, string[] roles, string[] permissions)
        {
            this.Identity = identity;
            this.roles = roles;
            this.permissions = permissions;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns> The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal. </returns>
        public IUserIdentity Identity { get; private set; }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        IIdentity IPrincipal.Identity
        {
            get { return this.Identity; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has any role.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has any role; otherwise, <c>false</c>.
        /// </value>
        public bool HasAnyRole
        {
            get { return this.roles.Any(); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has any permission.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has any permission; otherwise, <c>false</c>.
        /// </value>
        public bool HasAnyPermission
        {
            get { return this.permissions.Any(); }
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role"> The name of the role for which to check membership. </param>
        /// <returns> true if the current principal is a member of the specified role; otherwise, false. </returns>
        public bool IsInRole(string role)
        {
            return this.roles != null && this.roles.Any(r => r.Equals(role, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// true if the current principal is a member of the specified permission; otherwise, false.
        /// </returns>
        public bool IsInPermission(PermissionItem permission)
        {
            return this.IsInPermission(permission.PermissionId);
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// true if the current principal is a member of the specified permission; otherwise, false.
        /// </returns>
        public bool IsInPermission(string permission)
        {
            return this.permissions != null && this.permissions.Any(r => r.Equals(permission, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
