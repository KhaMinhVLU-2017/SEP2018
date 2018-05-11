using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Sep2018_MVC;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Admin/Staff
        [HttpPost]
        public ActionResult CheckOnline(HttpRequest request)
        {

            return View();
        }
        public ActionResult Notifi()
        {
            return View();
        }
        public ActionResult CreateSection()
        {
            SEP_2018_T6Entities db = new SEP_2018_T6Entities();
            List<Course> meoCourse = new List<Course>();
            meoCourse = db.Courses.ToList();
            List<Class> meoClass = new List<Class>();
            meoClass =  db.Classes.ToList();
            List<Subject> meoSubject = new List<Subject>();
            meoSubject = db.Subjects.ToList();

            ViewData["meoCourse"] = meoCourse;
            ViewData["meoClass"] = meoClass;
            ViewData["meoSubject"] = meoSubject;
            return View();
        }
        public ActionResult CreateAccount()
        {
            return View();
        }
    }
}