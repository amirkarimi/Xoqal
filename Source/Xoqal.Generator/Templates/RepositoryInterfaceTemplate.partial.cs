#region License
// RepositoryInterfaceTemplate.partial.cs
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

namespace Xoqal.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class RepositoryInterfaceTemplate
    {
        private GeneratorOptions options;
        private EntityInfo entityInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInterfaceTemplate" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="entityInfo">The entity info.</param>
        public RepositoryInterfaceTemplate(GeneratorOptions options, EntityInfo entityInfo)
        {
            this.options = options;
            this.entityInfo = entityInfo;
        }
    }
}
