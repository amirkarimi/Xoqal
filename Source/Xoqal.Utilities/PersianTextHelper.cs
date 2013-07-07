#region License
// PersianTextHelper.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Helps to use Persian text.
    /// </summary>
    public class PersianTextHelper
    {
        /// <summary>
        /// Gets the Arabic format.
        /// </summary>
        /// <param name="txt"> The Text. </param>
        /// <returns> </returns>
        public static string GetArabicsFormat(string txt)
        {
            return !string.IsNullOrEmpty(txt) ? txt.Replace("ی", "ي").Replace("ک", "ك") : txt;
        }

        /// <summary>
        /// Gets the Persian format.
        /// </summary>
        /// <param name="txt"> The Text. </param>
        /// <returns> </returns>
        public static string GetPersianFormat(string txt)
        {
            return !string.IsNullOrEmpty(txt) ? txt.Replace("ي", "ی").Replace("ك", "ک") : txt;
        }

        /// <summary>
        /// Converts all digits in the given string to the Persian digits.
        /// </summary>
        /// <param name="source"> </param>
        /// <returns> </returns>
        public static string ConvertToPersianDigit(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }
            
            var nums = new[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            for (var i = 0; i <= 9; i++)
            {
                source = source.Replace(i.ToString(), nums[i]);
            }

            return source;
        }
    }
}
