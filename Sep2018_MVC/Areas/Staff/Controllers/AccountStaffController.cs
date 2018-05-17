using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sep2018_MVC.Areas.Staff.Controllers
{
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
<<<<<<< HEAD

=======
        public ActionResult CreateAccount()
        {
            return View();
        }
        public ActionResult InformationAccountStaff()
        {
            return View();
        }
>>>>>>> 6e3d263b18114122ea01b735dda8908cac0e4bb4
    }
}