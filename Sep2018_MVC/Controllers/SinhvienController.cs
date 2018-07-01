using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class SinhvienController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Sinhvien
        public ActionResult Index()
        {
            //if(Session["username"] == null)
            if (string.IsNullOrEmpty((string)Session["username"]))
            {
                return RedirectToAction("Index", "Account");
            }
            var sj = db.Subjects.ToList();
            return View(sj);
        }
        [HttpGet]
        public ActionResult Index(string subject)
        {
            var sj = db.Subjects.ToList();
            return View(sj);
        }
    }
}