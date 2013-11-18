#region License
// MenuGeneratorTest.cs
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

namespace Xoqal.Tests.WebMvcTests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Xoqal.Web.Mvc.Controllers;
    using Xoqal.Web.Mvc.Security;

    [TestClass]
    public class MenuGeneratorTest
    {
        [TestMethod]
        public void GetMenuItemsWorksCorrectly()
        {
            // Arrange
            var menuGenerator = new MenuGenerator();

            // Act
            var menuItems = menuGenerator.GetMenuItems(Assembly.GetExecutingAssembly(), "Xoqal.Tests.WebMvcTests").ToList();

            // Assert
            var generalMenuItem = menuItems.FirstOrDefault(m => m.ActionName == "MyAction" && m.ControllerName == "General");
            Assert.IsNotNull(generalMenuItem);
            Assert.AreEqual(Resources.Resource.MenuItemTitle, generalMenuItem.Title);
            Assert.AreEqual(Resources.Resource.MenuItemCategory, generalMenuItem.Category);
            Assert.IsNotNull(generalMenuItem.PermissionAttribute);
            Assert.AreEqual("LoginUrl", generalMenuItem.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles", generalMenuItem.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds", generalMenuItem.PermissionAttribute.PermissionIds);

            var genralMenuItemOnAction = menuItems.FirstOrDefault(m => m.ActionName == "Home" && m.ControllerName == "General");
            Assert.IsNotNull(genralMenuItemOnAction);
            Assert.AreEqual("General", genralMenuItemOnAction.Title);
            Assert.AreEqual("Home", genralMenuItemOnAction.Category);
            Assert.IsNotNull(genralMenuItemOnAction.PermissionAttribute);
            Assert.AreEqual("LoginUrl", genralMenuItemOnAction.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles", genralMenuItemOnAction.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds", genralMenuItemOnAction.PermissionAttribute.PermissionIds);

            var genralMenuItemWithPermissionOnAction = menuItems.FirstOrDefault(m => m.ActionName == "Index" && m.ControllerName == "General2");
            Assert.IsNotNull(genralMenuItemWithPermissionOnAction);
            Assert.AreEqual("General2", genralMenuItemWithPermissionOnAction.Title);
            Assert.AreEqual("Home", genralMenuItemWithPermissionOnAction.Category);
            Assert.IsNotNull(genralMenuItemWithPermissionOnAction.PermissionAttribute);
            Assert.AreEqual("LoginUrl", genralMenuItemWithPermissionOnAction.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles", genralMenuItemWithPermissionOnAction.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds", genralMenuItemWithPermissionOnAction.PermissionAttribute.PermissionIds);

            var genralMenuItemAndPermissionOnAction = menuItems.FirstOrDefault(m => m.ActionName == "Index3" && m.ControllerName == "General3");
            Assert.IsNotNull(genralMenuItemAndPermissionOnAction);
            Assert.AreEqual("General3", genralMenuItemAndPermissionOnAction.Title);
            Assert.AreEqual("Home3", genralMenuItemAndPermissionOnAction.Category);
            Assert.IsNotNull(genralMenuItemAndPermissionOnAction.PermissionAttribute);
            Assert.AreEqual("LoginUrl3", genralMenuItemAndPermissionOnAction.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles3", genralMenuItemAndPermissionOnAction.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds3", genralMenuItemAndPermissionOnAction.PermissionAttribute.PermissionIds);

            var genralMenuItemOnActionWithPermission = menuItems.FirstOrDefault(m => m.ActionName == "Index4" && m.ControllerName == "General4");
            Assert.IsNotNull(genralMenuItemOnActionWithPermission);
            Assert.AreEqual("General4", genralMenuItemOnActionWithPermission.Title);
            Assert.AreEqual("Home4", genralMenuItemOnActionWithPermission.Category);
            Assert.IsNotNull(genralMenuItemOnActionWithPermission.PermissionAttribute);
            Assert.AreEqual("LoginUrl4", genralMenuItemOnActionWithPermission.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles4", genralMenuItemOnActionWithPermission.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds4", genralMenuItemOnActionWithPermission.PermissionAttribute.PermissionIds);

            var inheritedMenuItem = menuItems.FirstOrDefault(m => m.ActionName == "Index5" && m.ControllerName == "General5");
            Assert.IsNotNull(inheritedMenuItem);
            Assert.AreEqual("General5", inheritedMenuItem.Title);
            Assert.AreEqual("Home5", inheritedMenuItem.Category);
            Assert.IsNotNull(inheritedMenuItem.PermissionAttribute);
            Assert.AreEqual("LoginUrl5", inheritedMenuItem.PermissionAttribute.LoginUrl);
            Assert.AreEqual("Roles5", inheritedMenuItem.PermissionAttribute.Roles);
            Assert.AreEqual("PermissionIds5", inheritedMenuItem.PermissionAttribute.PermissionIds);
        }
    }

    [MenuItem(ActionMethodName = "Index", ResourceType = typeof(Resources.Resource), TitleResourceName = "MenuItemTitle", CategoryResourceName = "MenuItemCategory")]
    [Permission(PermissionIds = new[] { "PermissionIds" }, Roles = new[] { "Roles" }, LoginUrl = "LoginUrl")]
    public class GeneralController
    {
        [ActionName("MyAction")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        [MenuItem(ResourceType = typeof(Resources.Resource), Title = "General", Category = "Home")]
        public ActionResult Home()
        {
            throw new NotImplementedException();
        }
    }

    [MenuItem(ActionMethodName = "Index", ResourceType = typeof(Resources.Resource), Title = "General2", Category = "Home")]
    public class General2Controller
    {
        [Permission(PermissionIds = new[] { "PermissionIds" }, Roles = new[] { "Roles" }, LoginUrl = "LoginUrl")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }

    public class General3Controller
    {
        [MenuItem(ActionMethodName = "Index3", ResourceType = typeof(Resources.Resource), Title = "General3", Category = "Home3")]
        [Permission(PermissionIds = new[] { "PermissionIds3" }, Roles = new[] { "Roles3" }, LoginUrl = "LoginUrl3")]
        public ActionResult Index3()
        {
            throw new NotImplementedException();
        }
    }

    [Permission(PermissionIds = new[] { "PermissionIds4" }, Roles = new[] { "Roles4" }, LoginUrl = "LoginUrl4")]
    public class General4Controller
    {
        [MenuItem(ActionMethodName = "Index4", ResourceType = typeof(Resources.Resource), Title = "General4", Category = "Home4")]
        public ActionResult Index4()
        {
            throw new NotImplementedException();
        }
    }

    public class BaseClass
    {
        public ActionResult Index5()
        {
            throw new NotImplementedException();
        }
    }

    [Permission(PermissionIds = new[] { "PermissionIds5" }, Roles = new[] { "Roles5" }, LoginUrl = "LoginUrl5")]
    [MenuItem(ActionMethodName = "Index5", ResourceType = typeof(Resources.Resource), Title = "General5", Category = "Home5")]
    public class General5Controller : BaseClass
    {
    }
}
