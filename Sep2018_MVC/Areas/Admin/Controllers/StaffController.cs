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
        SEP_2018_T6Entities db = new SEP_2018_T6Entities();
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
           
            List<Course> meoCourse = new List<Course>();
            meoCourse = db.Courses.ToList();

            return View(meoCourse);
        }
        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult ClassMeo(int ?id)
        {
            List<object> meo = new List<object>();
            foreach(var item in db.Classes.Where(s=>s.FK_Course==id))
            {
                meo.Add(new { id_class= item.id, classname = item.ClassName});
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
    }
}