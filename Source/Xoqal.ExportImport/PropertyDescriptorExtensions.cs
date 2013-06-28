#region License
// PropertyDescriptorExtensions.cs
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

namespace Xoqal.ExportImport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// <see cref="PropertyDescriptor"/> class extensions.
    /// </summary>
    public static class PropertyDescriptorExtensions
    {
        /// <summary>
        /// Gets the specified property display name.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetDisplayName(this PropertyDescriptor property)
        {
            var displayName = property.Attributes.OfType<DisplayAttribute>().FirstOrDefault();
            return displayName == null ? property.DisplayName : displayName.GetName();
        }
    }
}
