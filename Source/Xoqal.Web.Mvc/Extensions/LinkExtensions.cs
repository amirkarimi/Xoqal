#region License
// LinkExtensions.cs
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
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// HTML link extensions.
    /// </summary>
    public static class LinkExtensions
    {
        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this HtmlHelper htmlHelper, string linkText, object routeValues = null, object htmlAttributes = null, bool encodeHtml = true)
        {
            return PersistRouteLink(htmlHelper, linkText, null, routeValues, htmlAttributes, encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values.
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="htmlAttributes"> The HTML attributes. </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string routeName,
            object routeValues = null,
            object htmlAttributes = null,
            bool encodeHtml = true)
        {
            return PersistRouteLink(
                htmlHelper, linkText, routeName, routeValues, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), encodeHtml);
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values by using a route name.
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="linkText"> The link text. </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <param name="htmlAttributes"> </param>
        /// <param name="encodeHtml"> </param>
        /// <returns> </returns>
        public static IHtmlString PersistRouteLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string routeName,
            object routeValues,
            IDictionary<string, object> htmlAttributes,
            bool encodeHtml)
        {
            var routeUrl = LinkExtensions.GeneratePersistRouteUrl(htmlHelper.ViewContext, routeName, routeValues);

            var linkTag = new TagBuilder("a") { InnerHtml = encodeHtml ? HttpUtility.HtmlEncode(linkText) : linkText };
            linkTag.Attributes.Add("href", routeUrl);
            linkTag.MergeAttributes(htmlAttributes);
            return new HtmlString(linkTag.ToString());
        }

        /// <summary>
        /// Generates a fully qualified URL for the specified route values by using a route name.
        /// </summary>
        /// <param name="viewContext"> </param>
        /// <param name="routeName"> Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <returns> </returns>
        internal static string GeneratePersistRouteUrl(ViewContext viewContext, string routeName, object routeValues)
        {
            var urlHelper = new UrlHelper(viewContext.RequestContext);
            HttpRequestBase request = viewContext.HttpContext.Request;

            // Merge route values
            var routeValueDictionary = new RouteValueDictionary(routeValues);
            var distinctRouteValues = viewContext.RouteData.Values
                .Where(rv =>
                    LinkExtensions.IsValidForPersistence(rv.Value) &&
                    !routeValueDictionary.ContainsKey(rv.Key))
                .ToArray();
            
            foreach (var routeValue in distinctRouteValues)
            {
                routeValueDictionary.Add(routeValue.Key, routeValue.Value);
            }

            // I don't use HttpUtility.ParseQueryString because it omits repeated query string keys which we need here
            var distinctQueryStrings = LinkExtensions.ParseQueryString(request.Url.Query)
                .Where(pair => 
                    !pair.Key.TrimStart().ToLower().StartsWith("x-") &&
                    !routeValueDictionary.ContainsKey(pair.Key) &&
                    !string.IsNullOrEmpty(pair.Value))
                .ToArray();

            string url = urlHelper.RouteUrl(routeName, routeValueDictionary, request.Url.Scheme, request.Url.Host);
            var uriBuilder = new UriBuilder(url);

            if (distinctQueryStrings.Any())
            {
                var query = uriBuilder.Query.TrimStart('?');
                uriBuilder.Query =
                    (string.IsNullOrEmpty(query) ? string.Empty : (query + "&")) +
                    string.Join("&", distinctQueryStrings.Select(q => string.Format("{0}={1}", q.Key, q.Value)));
            }

            return uriBuilder.ToString();
        }

        /// <summary>
        /// Parses the query string into not districted name value collection.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string queryString)
        {
            queryString = queryString.TrimStart('?').Trim();

            return queryString.Split('&')
                .Select(queryStringPart =>
                    queryStringPart.Split('='))
                .Where(keyValueStrings => keyValueStrings.Length > 0)
                .Select(keyValueStrings =>
                    new KeyValuePair<string, string>(
                        HttpUtility.UrlDecode(keyValueStrings[0]).Trim(),
                        keyValueStrings.Length < 2 ? string.Empty : HttpUtility.UrlDecode(keyValueStrings[1]).Trim()));
        }

        /// <summary>
        /// Gets a value indicates that the given value should persist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsValidForPersistence(object value)
        {
            Type valueType = value.GetType();

            return !(valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(DictionaryValueProvider<>));
        }
    }
}
