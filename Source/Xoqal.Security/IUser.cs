#region License
// IUser.cs
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
    using System.Security.Principal;

    public interface IUser
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the hashed password.
        /// </summary>
        /// <value>
        /// The hashed password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the last activity time.
        /// </summary>
        /// <value>
        /// The last activity time.
        /// </value>
        DateTime? LastActivityTime { get; set; }

        /// <summary>
        /// Gets or sets the last login time.
        /// </summary>
        /// <value>
        /// The last login time.
        /// </value>
        DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        IEnumerable<IRole> Roles { get; }
    }
}
