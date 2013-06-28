#region License
// HtmlPrefixScopeExtensions.cs
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
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlPrefixScopeExtensions
    {
        private const string IdsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

        /// <summary>
        /// Begins the scope of the collection items which uses GUID or ID fields and index hidden field instead of zero-based index.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, html.Encode(itemIndex)));

            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        }

        /// <summary>
        /// Begins the scope of the html field prefix which uses GUID or ID fields and index hidden field instead of zero-based index.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="htmlFieldPrefix"></param>
        /// <returns></returns>
        public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }

        /// <summary>
        /// Gets IDs.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        {
            // We need to use the same sequence of IDs following a server-side validation failure,  
            // otherwise the framework won't render the validation error messages next to each item.
            string key = IdsToReuseKey + collectionName;
            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                if (!string.IsNullOrEmpty(previouslyUsedIds))
                {
                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
                    {
                        queue.Enqueue(previouslyUsedId);
                    }
                }
            }

            return queue;
        }

        /// <summary>
        /// Represents the HTML field prefix scope.
        /// </summary>
        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo templateInfo;
            private readonly string previousHtmlFieldPrefix;

            /// <summary>
            /// Initializes a new instance of the <see cref="HtmlFieldPrefixScope"/> class.
            /// </summary>
            /// <param name="templateInfo"></param>
            /// <param name="htmlFieldPrefix"></param>
            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                this.templateInfo = templateInfo;

                this.previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                this.templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or
            /// resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this.templateInfo.HtmlFieldPrefix = this.previousHtmlFieldPrefix;
            }
        }
    }
}
