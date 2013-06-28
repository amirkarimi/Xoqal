#region License
// ResourceHelper.cs
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

namespace Xoqal.Utilities
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Helps to resolve the resource keys.
    /// </summary>
    public class ResourceHelper
    {
        /// <summary>
        /// Gets the resource value.
        /// </summary>
        /// <param name="resourceTypeName"> Name of the resource type. </param>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static string GetResourceValue(string resourceTypeName, string name)
        {
            return GetResourceValue(Type.GetType(resourceTypeName), name);
        }

        /// <summary>
        /// Gets the resource value.
        /// </summary>
        /// <param name="resourceType"> Type of the resource. </param>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public static string GetResourceValue(Type resourceType, string name)
        {
            var property = resourceType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            return property != null ? property.GetValue(null, null).ToString() : string.Empty;
        }
    }
}
