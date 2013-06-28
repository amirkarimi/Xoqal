#region License
// AjaxLinkExtensions.cs
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
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    /// <summary>
    /// AJAX link extensions.
    /// </summary>
    public static class AjaxLinkExtensions
    {
        #region Route Links

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> </param>
        /// <param name="ajaxOptions"> The ajax options. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper, string linkText, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            return PersistRouteLink(ajaxHelper, linkText, null, ajaxOptions, htmlAttributes);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="ajaxOptions"> The ajax options. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            object routeValues,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes = null,
            bool encodeHtml = true)
        {
            return PersistRouteLink(ajaxHelper, linkText, null, routeValues, ajaxOptions, htmlAttributes, encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="ajaxOptions"> The ajax options. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            object routeValues,
            AjaxOptions ajaxOptions,
            object htmlAttributes = null,
            bool encodeHtml = true)
        {
            return PersistRouteLink(
                ajaxHelper, linkText, null, routeValues, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="ajaxOptions"> The ajax options. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string routeName,
            AjaxOptions ajaxOptions,
            object htmlAttributes = null,
            bool encodeHtml = true)
        {
            return PersistRouteLink(
                ajaxHelper, linkText, null, null, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="ajaxOptions"> </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string routeName,
            object routeValues,
            AjaxOptions ajaxOptions,
            object htmlAttributes = null,
            bool encodeHtml = true)
        {
            return PersistRouteLink(
                ajaxHelper,
                linkText,
                routeName,
                routeValues,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values by using a route name.
        /// </summary>
        /// <param name="ajaxHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="ajaxOptions"> </param>
        /// <param name="htmlAttributes"> </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this AjaxHelper ajaxHelper,
            string linkText,
            string routeName,
            object routeValues,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes,
            bool encodeHtml)
        {
            var routeUrl = LinkExtensions.GeneratePersistRouteUrl(ajaxHelper.ViewContext, routeName, routeValues);
            return new HtmlString(GenerateLink(ajaxHelper, linkText, routeUrl, ajaxOptions, htmlAttributes, encodeHtml));
        }

        /// <summary>
        /// Generates an ajax link.
        /// </summary>
        /// <param name="ajaxHelper"> </param>
        /// <param name="linkText"> </param>
        /// <param name="targetUrl"> </param>
        /// <param name="ajaxOptions"> </param>
        /// <param name="htmlAttributes"> </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        private static string GenerateLink(
            AjaxHelper ajaxHelper,
            string linkText,
            string targetUrl,
            AjaxOptions ajaxOptions,
            IDictionary<string, object> htmlAttributes,
            bool encodeHtml)
        {
            var tagBuilder = new TagBuilder("a") { InnerHtml = encodeHtml ? HttpUtility.HtmlEncode(linkText) : linkText };

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", targetUrl);
            if (ajaxHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                tagBuilder.MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());
            }
            else
            {
                throw new Exception("Ajax RouteLinks are not supported when unobtrusive java script is disabled.");
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion
    }
}
