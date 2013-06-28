#region License
// ValidExtensionsAttribute.cs
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

namespace Xoqal.Utilities.Attributes
{
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class ValidExtensionsAttribute : Attribute
    {
        private readonly string extensionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidExtensionsAttribute" /> class.
        /// </summary>
        /// <param name="extensions"> The extensions. </param>
        public ValidExtensionsAttribute(string extensions)
        {
            this.extensionList = extensions;
        }

        /// <summary>
        /// Gets the extensions in comma separated format.
        /// </summary>
        /// <value> The extensions in comma separated format. </value>
        public string ExtensionList
        {
            get { return this.extensionList; }
        }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <returns> </returns>
        public string[] GetExtentions()
        {
            return this.extensionList.Split(',').Select(e => e.Trim().ToLower()).ToArray();
        }
    }
}
