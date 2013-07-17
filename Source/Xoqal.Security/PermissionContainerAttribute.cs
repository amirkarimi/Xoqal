#region License
// PermissionContainerAttribute.cs
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
    /// Represents meta-data for a permission container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class PermissionContainerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionItemAttribute" /> class.
        /// </summary>
        /// <param name="defaultResourceType">Default type of the resource.</param>
        public PermissionContainerAttribute(Type defaultResourceType)
        {
            this.DefaultResourceType = defaultResourceType;
        }

        /// <summary>
        /// Gets the default resource type of the permission container.
        /// </summary>
        /// <value>
        /// The default resource type of the permission container.
        /// </value>
        public Type DefaultResourceType { get; private set; }
    }
}
