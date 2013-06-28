#region License
// MultilanguageAttribute.cs
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

namespace Xoqal.Web.Mvc.Globalization
{
    using System.Web.Mvc;
    using Xoqal.Globalization;

    /// <summary>
    /// Multilanguage filter which sets the current language according to the route values.
    /// </summary>
    public class MultilanguageAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext"> The filter context. </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var language = (string)filterContext.RouteData.Values["language"];
            if (!string.IsNullOrWhiteSpace(language))
            {
                if (language.ToLower() == "default")
                {
                    LanguageManagement.SetLanguage(LanguageManagement.GetDefaultLanguage());
                }
                else
                {
                    LanguageManagement.SetLanguage(language);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
