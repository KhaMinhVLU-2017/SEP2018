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
    public class TeacherController : Controller
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

        public ActionResult ClassMeo(int? id,int? id_course)
        {
            List<object> meo = new List<object>();
            foreach (var item in db.Learnings.Where(s=>s.FK_Semester==id && s.Class.FK_Course==id_course))
            {
                meo.Add(new { id_class = item.Class.id, classname = item.Class.ClassName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SemesterMeo(int? id)
        {
            List<object> meo = new List<object>();
            foreach (var item in db.Schedules.Where(s=> s.FK_Course==id))
            {
                meo.Add(new { id_semes = item.Semester.id, semesName = item.Semester.SemesterName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subject(int? id_Class,int? id_semester)
        {
            List<object> meo = new List<object>();
            var listSubject = db.Learnings.Where(s => s.FK_Class == id_Class & s.FK_Semester== id_semester);
            foreach(var item in listSubject)
            {

                meo.Add(new { id_subject = item.Subject.id, subName=item.Subject.SubjectName });
              
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
    }
}