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
    public class SinhvienControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
         
            //Fake session for Controller
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["username"]).Returns(null); //somevalue
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);

            SinhvienController controller = new SinhvienController();
            controller.ControllerContext = mockControllerContext.Object;
            
            //Actual
            ActionResult result = controller.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));//Check Type
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");//Check redirect

        }
    }
}
