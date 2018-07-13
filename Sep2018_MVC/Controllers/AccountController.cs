using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class AccountController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pw,string returnURL)
        {
            var user = db.Users.FirstOrDefault(x => x.username == email);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.username, false);
                Session["id_user"] = user.username;
                Session["ava_user"] = user.avatar;
                if (Url.IsLocalUrl(returnURL) && returnURL.Length > 1 && returnURL.StartsWith("/")
                    && !returnURL.StartsWith("//") && !returnURL.StartsWith("/\\"))
                {
                    return Redirect(returnURL);
                }
                else if(User.IsInRole("Giao Vien"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Sinhvien");
                }
                
            }
            ModelState.AddModelError("", "Invalid User & Password");
            return View();
        }
        public ActionResult Logout()
        {
            Session["id_user"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}