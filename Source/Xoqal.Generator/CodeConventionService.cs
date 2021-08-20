#region License
// CodeConventionService.cs
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
    using System.Data.Entity.Design.PluralizationServices;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class CodeConventionService
    {
        private readonly PluralizationService pluralizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeConventionService" /> class.
        /// </summary>
        public CodeConventionService()
        {
            this.pluralizationService = PluralizationService.CreateService(new System.Globalization.CultureInfo("en"));
        }

        /// <summary>
        /// To the camel case.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public string ToCamelCase(string identifier)
        {
            if (identifier.Length > 1)
            {
                identifier = identifier.Substring(0, 1).ToLower() + identifier.Substring(1);
            }

            return identifier;
        }

        /// <summary>
        /// To the pascal case.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public string ToPascalCase(string identifier)
        {
            if (identifier.Length > 1)
            {
                identifier = identifier.Substring(0, 1).ToUpper() + identifier.Substring(1);
            }

            return identifier;
        }

        /// <summary>
        /// Pluralizes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string Pluralize(string name)
        {
            if (this.pluralizationService.IsPlural(name))
            {
                return name;
            }

            return this.pluralizationService.Pluralize(name);
        }

        /// <summary>
        /// Singularizes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string Singularize(string name)
        {
            if (this.pluralizationService.IsSingular(name))
            {
                return name;
            }

            return this.pluralizationService.Singularize(name);
        }
    }
}
