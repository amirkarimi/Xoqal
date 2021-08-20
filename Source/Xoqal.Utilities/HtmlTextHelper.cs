#region License
// HtmlTextHelper.cs
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
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public class HtmlTextHelper
    {
        /// <summary>
        /// Converts the specified HTML content to a plan text.
        /// </summary>
        /// <param name="html"> The HTML. </param>
        /// <returns> </returns>
        public static string ConvertHtmlToPlanText(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            
            var objRegEx = new Regex("<[^>]*>");
            string planText = objRegEx.Replace(html, string.Empty);
            planText = planText.Replace("&nbsp;", " ");
            planText = planText.Replace("&zwnj;", "‌");
            planText = planText.Replace("\r\n", " ").Trim();

            return planText;
        }

        /// <summary>
        /// Gets a plan text from HTML.
        /// </summary>
        /// <param name="html"> The HTML. </param>
        /// <param name="wordCount"> The word count. </param>
        /// <param name="addThreePoint"> if set to <c>true</c> adds three point. </param>
        /// <returns> </returns>
        public static string ConvertHtmlToPlanText(string html, int wordCount, bool addThreePoint)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }

            string planText = ConvertHtmlToPlanText(html);
            if (wordCount != 0)
            {
                int index = 0;
                for (int count = 0; count < wordCount && index != -1 && index + 1 < planText.Length; count++)
                {
                    index = planText.IndexOfAny(new[] { ' ', '\t', '\n' }, index + 1);
                }

                if (index > 0 && index < planText.Length)
                {
                    planText = planText.Substring(0, index);
                    if (addThreePoint)
                    {
                        planText += " ...";
                    }
                }
            }

            return planText;
        }

        /// <summary>
        /// Highlights the specified word in the given html content.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="wordCount">The word count.</param>
        /// <param name="addThreePoint">if set to <c>true</c> [add three point].</param>
        /// <param name="searchWord">The search word to be highlighted.</param>
        /// <param name="highlightCssStyle">The highlight CSS style.</param>
        /// <returns></returns>
        public static string Highlight(
            string html,
            int wordCount,
            bool addThreePoint,
            string searchWord,
            string highlightCssStyle = "background-color:#ffff66;font-weight:bold")
        {
            string planText = ConvertHtmlToPlanText(html, wordCount, addThreePoint);
            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                planText = planText.Replace(searchWord, string.Format("<span style=\"{0}\">{1}</span>", highlightCssStyle, searchWord));
            }

            return planText;
        }

        /// <summary>
        /// Removes the content of the tag with its content.
        /// </summary>
        /// <param name="source"> The source. </param>
        /// <param name="tagName"> Name of the tag. </param>
        /// <returns> </returns>
        private static string RemoveTagWithContent(string source, string tagName)
        {
            var regex = new Regex(
                string.Format("<{0}.*?/{0}>", tagName),
                RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
            return regex.Replace(source, string.Empty);
        }
    }
}
