using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sep2018_MVC.Areas.Admin.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Admin/Management
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DemoLogin()
        {
            return View();
        }


        public ActionResult LoginAdmin()
        {
            return View();
        }

        public ActionResult HomeAdmin()
        {
            return View();
        }
    }
}