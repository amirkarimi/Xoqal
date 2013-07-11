#region License
// PermissionItemAttribute.cs
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
    /// Represents a permission item.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class PermissionItemAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionItemAttribute"/> class.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="descriptionResourceName">Name of the description resource.</param>
        public PermissionItemAttribute(Type resourceType, string descriptionResourceName)
        {
            this.ResourceType = resourceType;
            this.DescriptionResourceName = descriptionResourceName;
        }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public Type ResourceType { get; private set; }

        /// <summary>
        /// Gets or sets the name of the description resource.
        /// </summary>
        /// <value>
        /// The name of the description resource.
        /// </value>
        public string DescriptionResourceName { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return Utilities.ResourceHelper.GetResourceValue(this.ResourceType, this.DescriptionResourceName);
            }
        }
    }
}
