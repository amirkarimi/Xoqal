#region License
// PermissionItem.cs
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
    using Utilities;

    /// <summary>
    /// Represents a permission entity.
    /// </summary>
    public class PermissionItem
    {
        private readonly string resourceName;
        private readonly Type resourceType;
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionItem" /> class.
        /// </summary>
        /// <param name="permissionId"> The permission id. </param>
        /// <param name="description"> The description. </param>
        public PermissionItem(string permissionId, string description)
        {
            this.PermissionId = permissionId.Trim();
            this.Description = description.Trim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionItem" /> class.
        /// </summary>
        /// <param name="permissoinId">The permission id. </param>
        /// <param name="resourceType">The resource type. </param>
        /// <param name="resourceName">The resource name. </param>
        public PermissionItem(string permissoinId, Type resourceType, string resourceName)
        {
            this.PermissionId = permissoinId;
            this.resourceType = resourceType;
            this.resourceName = resourceName;
        }

        /// <summary>
        /// Gets or sets the permission id.
        /// </summary>
        /// <value> The permission id. </value>
        public string PermissionId { get; private set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value> The description. </value>
        public string Description
        {
            get { return this.resourceType != null ? ResourceHelper.GetResourceValue(this.resourceType, this.resourceName) : this.description; }
            set { this.description = value; }
        }
    }
}
