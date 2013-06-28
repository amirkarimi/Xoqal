#region License
// StringLengthAttribute.cs
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
    /// Specifies the minimum and maximum length of characters that are allowed in a data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class StringLengthAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.StringLengthAttribute"/> class by using a specified maximum length.
        /// </summary>
        /// <param name="maximumLength">The maximum length of a string. </param>
        public StringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
            this.SetResources();
        }

        /// <summary>
        /// Sets the resources.
        /// </summary>
        private void SetResources()
        {
            if (ResourceManager.ResourceType != null)
            {
                this.ErrorMessageResourceType = ResourceManager.ResourceType;
                this.ErrorMessageResourceName = "DataAnnotations_StringLengthAttribute_ValidationError";
            }
        }
    }
}
