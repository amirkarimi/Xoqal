#region License
// ContentMultilanguageAttribute.cs
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

    /// <summary>
    /// Multilanguage filter which sets the current language for content according to the route values. It doesn't affect the UI or CurrentCulture/UICulture.
    /// </summary>
    public class ContentMultilanguageAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext"> The filter context. </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var language = (string)filterContext.RouteData.Values["contentLanguage"];
            if (!string.IsNullOrWhiteSpace(language))
            {
                ContentLanguageHelper.SetContentLanguage(language);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
