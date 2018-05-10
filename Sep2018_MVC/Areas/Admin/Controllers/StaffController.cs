using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sep2018_MVC.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Admin/Staff
        public ActionResult CheckOnline()
        {
            return View();
        }
        public ActionResult Notifi()
        {
            return View();
        }
        public ActionResult CreateSection()
        {
            return View();
        }
    }
}