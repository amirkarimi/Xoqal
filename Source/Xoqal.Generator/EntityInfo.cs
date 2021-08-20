#region License
// EntityInfo.cs
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

namespace Xoqal.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class EntityInfo
    {
        private readonly Type entityType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityInfo" /> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        public EntityInfo(Type entityType)
        {
            this.entityType = entityType;
            this.EntityName = entityType.Name;
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inherited entity.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is inherited entity; otherwise, <c>false</c>.
        /// </value>
        public bool IsInheritedEntity { get; set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
        public Type EntityType
        {
            get
            {
                return this.entityType;
            }
        }
    }
}
