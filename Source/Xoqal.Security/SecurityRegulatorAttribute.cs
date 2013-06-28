#region License
// SecurityRegulatorAttribute.cs
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
    /// Indicates that an assembly is a security regulator.
    /// </summary>
    /// <remarks>
    /// Security regulator can define the permissions for example.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class SecurityRegulatorAttribute : Attribute
    {
        private readonly Type permissionContainerType;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRegulatorAttribute" /> class.
        /// </summary>
        /// <param name="permissionContainerType"> The permission container. </param>
        public SecurityRegulatorAttribute(Type permissionContainerType)
        {
            this.permissionContainerType = permissionContainerType;
        }

        /// <summary>
        /// Gets the type of the permission container.
        /// </summary>
        /// <value> The type of the permission container. </value>
        public Type PermissionContainerType
        {
            get { return this.permissionContainerType; }
        }
    }
}
