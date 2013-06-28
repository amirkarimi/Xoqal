#region License
// GlobalizationUrlExtentions.cs
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

namespace Xoqal.Web.Mvc.Extensions
{
    using System.Web.Mvc;
    using Xoqal.Globalization;

    /// <summary>
    /// URL extensions to generate globalized URLs.
    /// </summary>
    public static class GlobalizationUrlExtentions
    {
        /// <summary>
        /// Converts a virtual path an application absolute path and format the specified path with the current language name.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns></returns>
        public static string GlobalizedContent(this UrlHelper urlHelper, string contentPath)
        {
            return urlHelper.Content(string.Format(contentPath, LanguageManagement.GetCurrentLanguage().CultureInfo.Name));
        }
    }
}
