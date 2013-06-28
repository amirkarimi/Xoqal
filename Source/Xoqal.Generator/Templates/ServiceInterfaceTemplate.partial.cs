#region License
// ServiceInterfaceTemplate.partial.cs
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

    public partial class ServiceInterfaceTemplate
    {
        private readonly GeneratorOptions options;
        private readonly EntityInfo entityInfo;
        private readonly CodeConventionService codeConventionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInterfaceTemplate" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="entityInfo">The entity info.</param>
        /// <param name="codeConventionService">The code convention service.</param>
        public ServiceInterfaceTemplate(GeneratorOptions options, EntityInfo entityInfo, CodeConventionService codeConventionService)
        {
            this.options = options;
            this.entityInfo = entityInfo;
            this.codeConventionService = codeConventionService;
        }
    }
}
