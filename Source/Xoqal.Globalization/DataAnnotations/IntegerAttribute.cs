#region License
// IntegerAttribute.cs
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

namespace Xoqal.Globalization.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Specifies that a data field value must be an integer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class IntegerAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerAttribute" /> class.
        /// </summary>
        /// <param name="allowNegatives">if set to <c>true</c> allows negatives integers.</param>
        public IntegerAttribute(bool allowNegatives = true)
            : base(GetPattern(allowNegatives))
        {
            this.SetResources();
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        /// <param name="allowNegatives">if set to <c>true</c> allows negatives integers.</param>
        /// <returns></returns>
        private static string GetPattern(bool allowNegatives)
        {
            return string.Format(@"^{0}[0-9]{{1,3}}(,[0-9]{{3}})*$|^{0}[0-9]*$", allowNegatives ? "-?" : string.Empty);
        }

        /// <summary>
        /// Sets the resources.
        /// </summary>
        private void SetResources()
        {
            if (ResourceManager.ResourceType != null)
            {
                this.ErrorMessageResourceType = ResourceManager.ResourceType;
                this.ErrorMessageResourceName = "DataAnnotations_IntegerAttribute_ValidationError";
            }
        }
    }
}
