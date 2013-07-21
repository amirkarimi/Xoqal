#region License
// LanguageContainer.cs
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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents search and load functionality to find all languages which defined in the driven projects.
    /// </summary>
    internal class LanguageContainer
    {
        private static readonly object SyncObject = new object();
        private static List<LanguageInfo> languages;

        /// <summary>
        /// Gets the languages.
        /// </summary>
        public static List<LanguageInfo> Languages
        {
            get
            {
                // Checking out of lock block to increase performance
                if (languages != null)
                {
                    return languages;
                }

                lock (SyncObject)
                {
                    if (languages == null)
                    {
                        LoadLanguages();
                    }

                    return languages;
                }
            }
        }

        /// <summary>
        /// Loads the languages.
        /// </summary>
        private static void LoadLanguages()
        {
            // Search among the assemblies which defined the GlobalizationRegulatorAttribute.
            IEnumerable<Assembly> assemblies =
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDefined(typeof(GlobalizationRegulatorAttribute), false));

            // Create new permission list
            languages = new List<LanguageInfo>();

            // Find LanguageContainerType in the found assemblies.
            foreach (Assembly assembly in assemblies)
            {
                var attribute =
                    (GlobalizationRegulatorAttribute)
                        assembly.GetCustomAttributes(typeof(GlobalizationRegulatorAttribute), false).FirstOrDefault();

                if (attribute != null)
                {
                    FieldInfo[] fields = attribute.LanguageContainerType.GetFields(BindingFlags.Public | BindingFlags.Static);

                    List<LanguageInfo> languageItems =
                        fields.Where(p => p.FieldType == typeof(LanguageInfo)).Select(p => (LanguageInfo)p.GetValue(null)).ToList();

                    // Add all found languages to permission list
                    languages.AddRange(languageItems);
                }
            }

            // Distinct the languages
            languages = languages.Distinct().ToList();

            Debug.WriteIf(languages.Count == 0, "LanguageContainer.LoadLanguages called but no permission found.");
        }
    }
}
