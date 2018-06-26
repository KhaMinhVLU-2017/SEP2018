using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class AccountController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email, string pw)
        {
            var user = db.Users.FirstOrDefault(x => x.username == email );
            if(user != null)
            {
                string s1 = pw;
                if (user.password.Trim().Equals(s1))
                {
                    Session["username"] = user.username;
                    Session["role"] = (int)user.PK_Position_Roles.Value;
                    Session["class"] = user.FK_Class;
                    if (user.PK_Position_Roles == 9)
                    {
                        return RedirectToAction("Index", "Giangvien");
                    }
                    if(user.PK_Position_Roles == 10 )
                    {
                        return RedirectToAction("Index", "Sinhvien");
                    }
                    if (user.PK_Position_Roles == 11)
                        return RedirectToAction("Index", "Giaovu");
                }else
                {
                    ViewBag.msg = "Quên luôn mật khẩu mới ác :v ";
                }
            }
            else
            {
                ViewBag.msg = "Có cái tên đăng nhập không è mà cũng quên -_-";
            }
            return View();
        }
        public ActionResult Logout()
        {
            if (Session["name"] != null)
            {
                Session["name"] = null;
            }
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}