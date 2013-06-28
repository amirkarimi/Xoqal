#region License
// ViewPageExtensions.cs
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
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.WebPages;

    /// <remarks>
    /// Idea from http://blog.logrythmik.com/post/A-Script-Block-Templated-Delegate-for-Inline-Scripts-in-Razor-Partials.aspx
    /// </remarks>
    public static class ViewPageExtensions
    {
        /// <summary>
        /// The Script Block Builder.
        /// </summary>
        private const string ScriptBlockBuilder = "ScriptBlockBuilder";

        /// <summary>
        /// Scripts the block.
        /// </summary>
        /// <param name="webPage">The web page.</param>
        /// <param name="key">The key.</param>
        /// <param name="template">The template.</param>
        /// <param name="renderOnAjaxRequest">if set to <c>true</c> [render on ajax request].</param>
        /// <returns></returns>
        public static IHtmlString ScriptBlock(
            this WebViewPage webPage,
            string key,
            Func<dynamic, HelperResult> template,
            bool renderOnAjaxRequest = true)
        {
            if (!renderOnAjaxRequest || !webPage.IsAjax)
            {
                var scripts = webPage.Context.Items[ScriptBlockBuilder] as Dictionary<string, string> ??
                    new Dictionary<string, string>();
                if (!scripts.ContainsKey(key))
                {
                    scripts.Add(key, template(null).ToHtmlString());
                }

                webPage.Context.Items[ScriptBlockBuilder] = scripts;
                return new MvcHtmlString(string.Empty);
            }

            return new MvcHtmlString(template(null).ToHtmlString());
        }

        /// <summary>
        /// Writes the script blocks.
        /// </summary>
        /// <param name="webPage">The web page.</param>
        /// <returns></returns>
        public static IHtmlString WriteScriptBlocks(this WebViewPage webPage)
        {
            var scripts = webPage.Context.Items[ScriptBlockBuilder] as Dictionary<string, string> ??
                new Dictionary<string, string>();
            var sb = new StringBuilder();
            foreach (var script in scripts)
            {
                sb.AppendLine(script.Value);
            }

            return new MvcHtmlString(sb.ToString());
        }
    }
}
