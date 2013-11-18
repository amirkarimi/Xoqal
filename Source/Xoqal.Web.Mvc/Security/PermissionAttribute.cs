#region License
// PermissionAttribute.cs
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

namespace Xoqal.Web.Mvc.Security
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xoqal.Security;

    /// <summary>
    /// Represents an attribute that is used to restrict access by callers to an action method.
    /// </summary>
    public class PermissionAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAttribute" /> class.
        /// </summary>
        public PermissionAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAttribute" /> class.
        /// </summary>
        /// <param name="permissionIds"> The permission ids. </param>
        public PermissionAttribute(params string[] permissionIds)
        {
            this.PermissionIds = permissionIds;
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets the permission ids.
        /// </summary>
        /// <value>
        /// The permission ids.
        /// </value>
        public string[] PermissionIds { get; set; }

        /// <summary>
        /// Gets or sets the login URL.
        /// </summary>
        /// <value> The login URL. </value>
        public string LoginUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is role required.
        /// </summary>
        /// <value> <c>true</c> if this instance is role required; otherwise, <c>false</c> . </value>
        public bool IsRoleRequired { get; set; }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext"> The filter context. </param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            this.OnAuthorization(filterContext, false);
        }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="suppressCustomLoginRedirect"></param>
        public void OnAuthorization(AuthorizationContext filterContext, bool suppressCustomLoginRedirect)
        {
            // Skipping the authorization according to the AllowAnonymousAttribute has been taken from http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/10f94d855cc9#src%2fSystem.Web.Mvc%2fAuthorizeAttribute.cs
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                                 || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            var user = Authentication.Default.GetCurrentUserPrincipal();
            if (!this.IsAuthorized(user))
            {
                filterContext.Result = new HttpUnauthorizedResult();

                if (!string.IsNullOrWhiteSpace(this.LoginUrl) && !suppressCustomLoginRedirect)
                {
                    string loginUrl = VirtualPathUtility.ToAbsolute(this.LoginUrl);
                    loginUrl = loginUrl + (this.LoginUrl.Contains("?") ? "&" : "?") + "ReturnUrl=" +
                        HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery);
                    filterContext.HttpContext.Response.Redirect(loginUrl);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is authorized for the current permission attribute instance or not.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsAuthorized(IUserPrincipal user)
        {
            if (user == null ||
                !this.HasPermissions(user) ||
                !this.HasRoles(user) ||
                (this.IsRoleRequired && !user.HasAnyRole))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the specified user has the specified permissions.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private bool HasPermissions(IUserPrincipal user)
        {
            return this.PermissionIds == null || this.PermissionIds.Any(user.IsInPermission);
        }

        /// <summary>
        /// Checks if the specified user has the specified roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private bool HasRoles(IUserPrincipal user)
        {
            return this.Roles == null || this.Roles.Any(user.IsInRole);
        }
    }
}
