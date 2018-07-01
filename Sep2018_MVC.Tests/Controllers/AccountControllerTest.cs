using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sep2018_MVC;
using Sep2018_MVC.Controllers;

namespace Sep2018_MVC.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void viewIndex() //CheckView Main Login
        {
            //Arrange 
            AccountController controller = new AccountController();
            //Actual
            ActionResult result = controller.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Login_Student()     //Check Login in views
        {
            //Arrange
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["username"]).Returns("t1305"); //session
            mockSession.SetupGet(s => s["role"]).Returns("10");     //session
            mockSession.SetupGet(s => s["class"]).Returns("3");     //session
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);

            AccountController controller = new AccountController();
            controller.ControllerContext = mockControllerContext.Object;

            //Actual
            var result = controller.Index("t1305", "toor");
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));//Check type
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");//Check Route\
        }
        [TestMethod]
        public void Login_Teacher()     //Check Login in views
        {
            //Arrange
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["username"]).Returns("gv8989"); //session
            mockSession.SetupGet(s => s["role"]).Returns("9");     //session
            mockSession.SetupGet(s => s["class"]).Returns("3");     //session
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);

            AccountController controller = new AccountController();
            controller.ControllerContext = mockControllerContext.Object;

            //Actual
            var result = controller.Index("gv8989", "toor");
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));//Check type
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");//Check Route\
        }
        [TestMethod]
        public void Logout()
        {
            //Arrange
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["username"]).Returns("t150737"); //session
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);

            AccountController controller = new AccountController();
            controller.ControllerContext = mockControllerContext.Object;
            //Actual
            var result = controller.Logout();
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));//Check type
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");//Check Route\

        }
    }
}