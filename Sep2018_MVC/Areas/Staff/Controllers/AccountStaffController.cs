﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sep2018_MVC.Areas.Staff.Controllers
{
    [Authorize]
    public class AccountStaffController : Controller
    {
        // GET: Staff/AccountStaff
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult ChangePassword()
        {
            return View();
        }


        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult InformationAccountStaff()
        {
            return View();
        }

    }

}
