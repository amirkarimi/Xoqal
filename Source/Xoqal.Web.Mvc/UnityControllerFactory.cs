#region License
// UnityControllerFactory.cs
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

namespace Xoqal.Web.Mvc
{
    using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

    /// <summary>
    /// Represents a controller factory integrated with Unity.
    /// </summary>
    /// <remarks>
    /// With help of http://weblogs.asp.net/shijuvarghese/archive/2011/01/06/developing-web-apps-using-asp-net-mvc-3-razor-and-ef-code-first-part-1.aspx
    /// </remarks>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private const string HttpContextKey = "UnityControllerFactory";
        private static readonly object SyncObject = new object();
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityControllerFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityControllerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the bags.
        /// </summary>
        /// <value>
        /// The bags.
        /// </value>
        private Dictionary<IController, UnityControllerFactoryBag> Bags
        {
            get
            {
                var bags = HttpContext.Current.Items[HttpContextKey] as Dictionary<IController, UnityControllerFactoryBag>;
                if (bags != null)
                {
                    return bags;
                }

                lock (SyncObject)
                {
                    bags = HttpContext.Current.Items[HttpContextKey] as Dictionary<IController, UnityControllerFactoryBag>;
                    if (bags == null)
                    {
                        bags = new Dictionary<IController, UnityControllerFactoryBag>();
                        HttpContext.Current.Items[HttpContextKey] = bags;
                    }

                    return bags;
                }
            }
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
            this.Bags[controller].Container.Dispose();
        }

        /// <summary>
        /// Gets the controller instance.
        /// </summary>
        /// <param name="reqContext"> The request context. </param>
        /// <param name="controllerType"> Type of the controller. </param>
        /// <returns> </returns>
        protected override IController GetControllerInstance(RequestContext reqContext, Type controllerType)
        {
            IController controller;
            if (controllerType == null || !typeof(IController).IsAssignableFrom(controllerType))
            {
                return base.GetControllerInstance(reqContext, controllerType);
            }

            try
            {
                // Create new bag
                var bag = new UnityControllerFactoryBag();
                bag.Container = this.container.CreateChildContainer();

                controller = bag.Container.Resolve(controllerType) as IController;
                
                this.Bags.Add(controller, bag);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Error resolving controller {0}", controllerType.Name), ex);
            }

            return controller;
        }
    }
}
