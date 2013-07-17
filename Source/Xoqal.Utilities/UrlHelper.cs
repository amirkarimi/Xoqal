#region License
// UrlHelper.cs
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
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provide some helpers static method toward of URL.
    /// </summary>
    public static class UrlHelper
    {
        private static readonly Regex Feet = new Regex(@"([0-9]\s?)'([^'])", RegexOptions.Compiled);
        private static readonly Regex Inch1 = new Regex(@"([0-9]\s?)''", RegexOptions.Compiled);
        private static readonly Regex Inch2 = new Regex(@"([0-9]\s?)""", RegexOptions.Compiled);
        private static readonly Regex Num = new Regex(@"#([0-9]+)", RegexOptions.Compiled);
        private static readonly Regex Dollar = new Regex(@"[$]([0-9]+)", RegexOptions.Compiled);
        private static readonly Regex Percent = new Regex(@"([0-9]+)%", RegexOptions.Compiled);
        private static readonly Regex Sep = new Regex(@"[\s_/\\+:.]", RegexOptions.Compiled);

        // included Persian char(s)
        private static readonly Regex Empty = new Regex(@"[^-A-Za-z0-9\u0600-\u06FF]", RegexOptions.Compiled);
        private static readonly Regex Extra = new Regex(@"[-]+", RegexOptions.Compiled);

        /// <summary>
        /// Gets readable encoded URL from the specified raw URL.
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        /// <remarks>Persian characters included.</remarks>
        public static string GetReadableEncodedUrl(string rawUrl)
        {
            if (!string.IsNullOrEmpty(rawUrl))
            {
                rawUrl = rawUrl.Trim().ToLower();
                rawUrl = rawUrl.Replace("&", "and");

                rawUrl = Feet.Replace(rawUrl, "$1-ft-");
                rawUrl = Inch1.Replace(rawUrl, "$1-in-");
                rawUrl = Inch2.Replace(rawUrl, "$1-in-");
                rawUrl = Num.Replace(rawUrl, "num-$1");

                rawUrl = Dollar.Replace(rawUrl, "$1-dollar-");
                rawUrl = Percent.Replace(rawUrl, "$1-percent-");

                rawUrl = Sep.Replace(rawUrl, "-");

                rawUrl = Empty.Replace(rawUrl, string.Empty);
                rawUrl = Extra.Replace(rawUrl, "-");

                rawUrl = rawUrl.Trim('-');
            }

            return rawUrl;
        }
    }
}
