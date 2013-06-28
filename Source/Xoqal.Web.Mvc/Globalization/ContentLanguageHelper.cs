#region License
// ContentLanguageHelper.cs
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

namespace Xoqal.Web.Mvc.Globalization
{
    using System.Web;
    using Xoqal.Globalization;

    /// <summary>
    /// Helps to set or get the content language.
    /// </summary>
    public class ContentLanguageHelper
    {
        /// <summary>
        /// The Content Language Session Key.
        /// </summary>
        public const string ContentLanguageSessionKey = "ContentLanguage";

        /// <summary>
        /// Sets the content language.
        /// </summary>
        /// <param name="language"> </param>
        public static void SetContentLanguage(string language)
        {
            SetContentLanguage(LanguageManagement.GetLanguage(language));
        }

        /// <summary>
        /// Sets the content language.
        /// </summary>
        /// <param name="language"> </param>
        public static void SetContentLanguage(LanguageInfo language)
        {
            HttpContext.Current.Session[ContentLanguageSessionKey] = language;
        }

        /// <summary>
        /// Gets the current content language.
        /// </summary>
        /// <returns> </returns>
        public static LanguageInfo GetContentLanguage()
        {
            LanguageInfo currentLanguage = HttpContext.Current.Session[ContentLanguageSessionKey] as LanguageInfo ??
                LanguageManagement.GetDefaultLanguage();

            return currentLanguage;
        }
    }
}
