#region License
// ActionExtenstions.cs
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
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Represents the action extensions.
    /// </summary>
    public static class ActionExtenstions
    {
        /// <summary>
        /// Determines whether is current executing action equals to the given one.
        /// </summary>
        /// <param name="htmlHelper"> The HTML helper. </param>
        /// <param name="actionName"> Name of the action. </param>
        /// <param name="controllerName"> Name of the controller. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <returns> <c>true</c> if the current executing action equals to the given one; otherwise, <c>false</c> . </returns>
        /// <remarks>
        /// Got from http://stackoverflow.com/questions/362514/how-can-i-return-the-current-action-in-an-asp-net-mvc-view but changed by A. Karimi
        /// </remarks>
        public static bool IsCurrentAction(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues = null)
        {
            bool result = false;
            string normalizedControllerName = controllerName.EndsWith("Controller")
                ? controllerName
                : string.Format("{0}Controller", controllerName);

            if (htmlHelper.ViewContext == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(actionName))
            {
                return false;
            }

            var controller = htmlHelper.ViewContext.Controller;

            if (controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase) &&
                controller.ValueProvider.GetValue("action").AttemptedValue.Equals(actionName, StringComparison.InvariantCultureIgnoreCase) &&
                    HasRouteValues(htmlHelper, routeValues))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the current route values and the given one have the same values.
        /// </summary>
        /// <param name="htmlHelper"> </param>
        /// <param name="routeValues"> </param>
        /// <returns> </returns>
        private static bool HasRouteValues(HtmlHelper htmlHelper, object routeValues)
        {
            if (routeValues != null)
            {
                foreach (var routeValue in new RouteValueDictionary(routeValues))
                {
                    if (htmlHelper.ViewContext.RouteData.Values[routeValue.Key] == null ||
                        !htmlHelper.ViewContext.RouteData.Values[routeValue.Key].ToString().Equals(
                            routeValue.Value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
