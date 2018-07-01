using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sep2018_MVC;
using Sep2018_MVC.Controllers;

namespace Sep2018_MVC.Tests.Controllers
{
    [TestClass]
    public class GiangvienControllerTest
    {
        [TestMethod]
        public void Index() // check type
        {
            //Arrange
            GiangvienController controller = new GiangvienController();
            //Actual
            ActionResult result = controller.Index();
            //Assrert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Check_Index()   // Check empty
        {
            //Arrange
            GiangvienController controller = new GiangvienController();
            //Actual
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
