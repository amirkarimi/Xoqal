#region License
// GlobalizationRegulatorAttribute.cs
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

namespace Xoqal.Globalization
{
    using System;

    /// <summary>
    /// Indicates that an assembly is a globalization regulator.
    /// </summary>
    /// <remarks>
    /// Globalization regulator assembly can define the supported languages for example.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class GlobalizationRegulatorAttribute : Attribute
    {
        private readonly Type languageContainerType;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalizationRegulatorAttribute" /> class.
        /// </summary>
        /// <param name="languageContainerType"> The language container. </param>
        public GlobalizationRegulatorAttribute(Type languageContainerType)
        {
            this.languageContainerType = languageContainerType;
        }

        /// <summary>
        /// Gets the type of the language container.
        /// </summary>
        /// <value> The type of the language container. </value>
        public Type LanguageContainerType
        {
            get { return this.languageContainerType; }
        }
    }
}
