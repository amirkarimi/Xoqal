#region License
// LanguageManagement.cs
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Represents the functionality of the multilanguage management.
    /// </summary>
    public class LanguageManagement
    {
        #region Get Methods

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <returns> </returns>
        public static List<LanguageItem> GetAllLanguages()
        {
            return LanguageContainer.Languages;
        }

        /// <summary>
        /// Gets the default language.
        /// </summary>
        /// <returns> </returns>
        public static LanguageItem GetDefaultLanguage()
        {
            return GetAllLanguages().OrderBy(l => l.Order).First();
        }

        /// <summary>
        /// Gets the language by its name.
        /// </summary>
        /// <param name="name"> The language name. </param>
        /// <returns> </returns>
        public static LanguageItem GetLanguage(string name)
        {
            return GetLanguage(CultureInfo.GetCultureInfo(name));
        }

        /// <summary>
        /// Gets the current language.
        /// </summary>
        /// <returns> </returns>
        public static LanguageItem GetCurrentLanguage()
        {
            return GetLanguage(Thread.CurrentThread.CurrentUICulture);
        }

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets current language.
        /// </summary>
        /// <param name="language"> The language item. </param>
        public static void SetLanguage(LanguageItem language)
        {
            SetLanguage(language.CultureInfo);
        }

        /// <summary>
        /// Sets current language.
        /// </summary>
        /// <param name="name"> The name. </param>
        public static void SetLanguage(string name)
        {
            SetLanguage(GetLanguage(name));
        }

        /// <summary>
        /// Set current language.
        /// </summary>
        /// <param name="cultureInfo"> The CultureInfo object. </param>
        public static void SetLanguage(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the language by the culture information.
        /// </summary>
        /// <param name="cultureInfo"> The CultureInfo object. </param>
        /// <returns> </returns>
        private static LanguageItem GetLanguage(CultureInfo cultureInfo)
        {
            LanguageItem language = LanguageContainer.Languages.Where(l => l.CultureInfo == cultureInfo).FirstOrDefault();
            if (language == null)
            {
                throw new LanguageNotSupportedException(cultureInfo);
            }

            return language;
        }

        #endregion
    }
}
