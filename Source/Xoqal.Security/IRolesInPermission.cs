#region License
// IRolesInPermission.cs
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

    /// <summary>
    /// Represents a conceptual relation between roles and permissions.
    /// </summary>
    public interface IRolesInPermission
    {
        /// <summary>
        /// Gets or sets the role ID.
        /// </summary>
        /// <value>
        /// The role ID.
        /// </value>
        Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the permission ID.
        /// </summary>
        /// <value>
        /// The permission ID.
        /// </value>
        string PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        IRole Role { get; set; }
    }
}
