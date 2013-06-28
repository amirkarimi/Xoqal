#region License
// AuthorizationExtensions.cs
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
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    
    /// <summary>
    /// Authorization extension.
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified action is authorized.
        /// </summary>
        /// <remarks>
        /// The code base is gotten from http://vivien-chevallier.com/Articles/create-an-authorized-action-link-extension-for-aspnet-mvc-3
        /// </remarks>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool IsActionAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName = null)
        {
            ControllerBase controllerBase = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.Controller : GetControllerByName(htmlHelper, controllerName);
            ControllerContext controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null)
            {
                return false;
            }

            FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (IAuthorizationFilter authorizationFilter in filters.AuthorizationFilters)
            {
                if (authorizationFilter is Security.PermissionAttribute)
                {
                    ((Security.PermissionAttribute)authorizationFilter).OnAuthorization(authorizationContext, true);
                }
                else
                {
                    authorizationFilter.OnAuthorization(authorizationContext);
                }

                if (authorizationContext.Result != null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets an <see cref="ControllerBase"/> instance according to the specified controller name.
        /// </summary>
        /// <remarks>
        /// The code base is gotten from http://vivien-chevallier.com/Articles/create-an-authorized-action-link-extension-for-aspnet-mvc-3
        /// </remarks>
        /// <param name="htmlHelper"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        private static ControllerBase GetControllerByName(HtmlHelper htmlHelper, string controllerName)
        {
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);
            if (controller == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The IControllerFactory '{0}' did not return a controller for the name '{1}'.", factory.GetType(), controllerName));
            }

            return (ControllerBase)controller;
        }
    }
}
