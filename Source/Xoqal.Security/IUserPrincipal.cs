#region License
// IUserPrincipal.cs
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
    using System.Security.Principal;
    using System.Text;

    /// <summary>
    /// Defines a user principal.
    /// </summary>
    public interface IUserPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        new IUserIdentity Identity { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has any role.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has any role; otherwise, <c>false</c>.
        /// </value>
        bool HasAnyRole { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has any permission.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has any permission; otherwise, <c>false</c>.
        /// </value>
        bool HasAnyPermission { get; }

        /// <summary>
        /// Determines whether the current principal belongs to the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// <c>true</c> if the current principal belongs to the specified permission; otherwise, <c>false</c>.
        /// </returns>
        bool IsInPermission(PermissionItem permission);

        /// <summary>
        /// Determines whether the current principal belongs to the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// <c>true</c> if the current principal belongs to the specified permission; otherwise, <c>false</c>.
        /// </returns>
        bool IsInPermission(string permission);
    }
}
