using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Sep2018_MVC;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Areas.Staff.Controllers
{
    public class AccountController : Controller
    {
        // GET: Staff/Account
        public ActionResult Index()
        {
            return View();
        }
    }
}