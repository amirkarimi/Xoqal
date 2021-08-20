#region License
// FileHelper.cs
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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Attributes;

    /// <summary>
    /// 
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Gets the valid extensions.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> </returns>
        public static string[] GetValidExtensions(object item)
        {
            Type enumType = item.GetType();
            FieldInfo field = enumType.GetField(item.ToString());
            var attr = (ValidExtensionsAttribute)field.GetCustomAttributes(typeof(ValidExtensionsAttribute), true).FirstOrDefault();

            if (attr == null)
            {
                return new string[0];
            }

            return attr.GetExtentions();
        }

        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="relativePath"> The relative path. </param>
        /// <returns> </returns>
        public static string GetPhysicalPath(string relativePath)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                // Not Web
                return Path.GetFullPath(relativePath);
            }

            // Web
            return httpContext.Server.MapPath(relativePath);
        }

        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetFileType<T>(string fileName)
            where T : struct
        {
            string extension = Path.GetExtension(fileName).Remove(0, 1).Trim().ToLower();
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                var enumValueExtensions = FileHelper.GetValidExtensions(enumValue);
                if (enumValueExtensions.Contains(extension))
                {
                    return (T)enumValue;
                }
            }

            T otherValue;
            if (Enum.TryParse("Other", out otherValue))
            {
                return otherValue;
            }

            throw new ArgumentException("The file name extension could not be matched to any of the specified enum items and the enum doesn't have an 'Other' item.");
        }
    }
}
