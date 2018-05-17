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
    public class TeacherController : Controller
    {
        // GET: Admin/Staff
        SEP_2018_T6Entities db = new SEP_2018_T6Entities();
        [HttpPost]
        public ActionResult CheckOnline(int? txt_course,int? txt_scheduledetail, int? txt_lesson, int? txt_semester, int? txt_class, int? txt_subject, DateTime txt_day, TimeSpan? txt_timefrom, TimeSpan? txt_timeto)
        {
            Attendance meo = new Attendance();
            meo.Date = txt_day;
            meo.BeginTime = txt_timefrom;
            meo.EndTime = txt_timeto;
            meo.Lesson = txt_lesson;
            meo.Unit_Lession = db.ScheduleDetails.FirstOrDefault(s => s.id == txt_scheduledetail).Unit_Lession;
            meo.FK_ScheduleDetail = txt_scheduledetail;
            return View();
        }
        [HttpGet]
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

            List<Course> meoCourse = new List<Course>();
            meoCourse = db.Courses.ToList();
            return View(meoCourse);
        }
        public ActionResult CreateAccount()
        {
            return View();
        }
        /*
         * Ajax field from views CreateSection
         * 
         */
        public ActionResult ClassMeo(int? id, int? id_course)
        {
            List<object> meo = new List<object>();
            foreach (var item in db.Classes.Where(s => s.FK_Course == id_course && s.Course.FK_Semester == id))
            {
                meo.Add(new { id_class = item.id, classname = item.ClassName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SemesterMeo(int? id)
        {
            List<object> meo = new List<object>();
            foreach (var item in db.Schedules.Where(s => s.FK_Course == id))
            {
                meo.Add(new { id_semes = item.Semester.id, semesName = item.Semester.SemesterName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subject(int? id_Class, int? id_semester)
        {
            List<object> meo = new List<object>();
            var listSubject = db.Learnings.Where(s => s.FK_Class == id_Class & s.FK_Semester == id_semester);
            foreach (var item in listSubject)
            {

                meo.Add(new { id_subject = item.Subject.id, subName = item.Subject.SubjectName });

            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        /*
         * Ajax ScheduleDetail
         */
        public ActionResult ScheDetail(int? id_subject, int? id_course, int? id_semester)
        {
            var id_schedule = db.Schedules.FirstOrDefault(s => s.FK_Course == id_course && s.FK_Semester == id_semester).id;//get ID schedule
            var detail = db.ScheduleDetails.Where(s => s.FK_Subject == id_subject && s.FK_Schedule == id_schedule);//get object Detail Schedule
            List<object> meo = new List<object>();//Create Json return Views
            foreach (var item in detail)
            {
                meo.Add(new { id = item.id, BeginTime = item.BeginTime, EndTime = item.EndTime, Lession = item.Lession, Unit_Lession = item.Unit_Lession, SubjectName = db.Subjects.FirstOrDefault(s => s.id == item.FK_Subject).SubjectName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Course()
        {
            return View();
        }
        public ActionResult InformationAccount()
        {
            return View();
        }
    }
}