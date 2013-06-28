#region License
// RangeAttribute.cs
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
    /// Specifies the numeric range constraints for the value of a data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RangeAttribute : System.ComponentModel.DataAnnotations.RangeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute" /> class.
        /// </summary>
        /// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
        public RangeAttribute(double minimum, double maximum)
            : base(minimum, maximum)
        {
            this.SetResources();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute" /> class.
        /// </summary>
        /// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
        public RangeAttribute(int minimum, int maximum)
            : base(minimum, maximum)
        {
            this.SetResources();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute" /> class.
        /// </summary>
        /// <param name="type">Specifies the type of the object to test.</param>
        /// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
        public RangeAttribute(Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
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
                this.ErrorMessageResourceName = "DataAnnotations_RangeAttribute_ValidationError";
            }
        }
    }
}
