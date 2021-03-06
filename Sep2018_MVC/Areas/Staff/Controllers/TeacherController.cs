﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Sep2018_MVC;
using Sep2018_MVC.Models;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Sep2018_MVC.Areas.Staff.Controllers
{
    [Authorize (Roles ="Giao Vien")]
    public class TeacherController : Controller
    {
   
        //SEP_2018_T6Entities db = new SEP_2018_T6Entities();

        // GET: Admin/Staff
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();

        [HttpPost]
        public ActionResult CheckOnline(int? txt_course,int? txt_scheduledetail, int txt_lesson, int? txt_semester, int? txt_class, int? txt_subject, DateTime txt_day, TimeSpan? txt_timefrom, TimeSpan? txt_timeto)
        {
            Attendance meo = new Attendance();
            meo.Date = txt_day;
            meo.BeginTime = txt_timefrom;
            meo.EndTime = txt_timeto;
            meo.Lesson =Math.Abs(txt_lesson);
            meo.Unit_Lession = db.ScheduleDetails.FirstOrDefault(s => s.id == txt_scheduledetail).Unit_Lession;
            meo.FK_ScheduleDetail = txt_scheduledetail;
            db.Attendances.Add(meo);
            db.SaveChanges();

            ViewData["user"] = db.Users.Where(s => s.FK_Class == txt_class).ToList();
            ViewData["AttenType"] = db.AttendanceTypes.ToList();
            return View(meo);
        }
        public ActionResult CreateSection()
        {
            string id_Teacher = Session["id_user"].ToString();
            List<ScheduleDetail> listScheduleDetail = new List<ScheduleDetail>();
            listScheduleDetail = db.ScheduleDetails.Where(s => s.FK_User_GV == id_Teacher).ToList();
            List<int> listShe = new List<int>();
            foreach (var item in listScheduleDetail)
            {
                int meo;
                meo = (int)db.Schedules.Find(item.FK_Schedule).FK_Course;
                listShe.Add(meo);
            }
            List<Course> mon = new List<Course>();//Save List Course's Teacher
            foreach (var item in listShe.Distinct())
            {
                Course su = new Course();
                su = db.Courses.Find(item);
                mon.Add(su);
            }

            //List<Course> meoCourse = new List<Course>();
            //meoCourse = db.Courses.ToList();
            return View(mon);
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
        public ActionResult Subject(int? id_Class, int? id_course)
        {
            List<object> meo = new List<object>();
            var listSubject = db.Learnings.Where(s => s.FK_Class == id_Class & s.Class.FK_Course==db.Courses.FirstOrDefault(d=>d.id==id_course).id);
            foreach (var item in listSubject)
            {

                meo.Add(new { id_subject = item.Subject.id, subName = item.Subject.SubjectName });

            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        /*
         * Ajax ScheduleDetail
         */
         //post Session of Teacher for get ScheduleDetail's teacher
        public ActionResult ScheDetail(int? id_subject, int? id_course, int? id_semester,int? id_class,string sessionLove)
        {
            var id_schedule = db.Schedules.FirstOrDefault(s => s.FK_Course == id_course && s.FK_Semester == id_semester).id;//get ID schedule
            int id_learning = db.Learnings.FirstOrDefault(s => s.FK_Subject == id_subject && s.FK_Semester == id_semester && s.FK_Class == id_class).id;
            var detail = db.ScheduleDetails.Where(s => s.FK_Learning==id_learning && s.FK_User_GV==sessionLove);//get object Detail Schedule
            List<object> meo = new List<object>();//Create Json return Views
            foreach (var item in detail)
            {
                meo.Add(new { id = item.id, BeginTime = item.BeginTime, EndTime = item.EndTime, Lession = item.Lession, Unit_Lession = item.Unit_Lession, SubjectName = db.Subjects.FirstOrDefault(s => s.id == id_subject).SubjectName });
            }
            return Json(meo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Course()// Can't test because Action is Not Complete 
        {
            return View();
        }
        public ActionResult InformationAccount()
        {
            string id_User = Session["id_user"].ToString();
            Person teacher = new Person();
            teacher = db.People.FirstOrDefault(s => s.MS == id_User);
            TempData["User_Teacher"] = db.Users.Find(id_User);
            return View(teacher);
        }
        public ActionResult History()
        {
            List<Attendance> meo = db.Attendances.ToList();
            return View(meo);
        }

        public ActionResult EditAttend(int id)
        {
            Attendance meo = db.Attendances.Find(id);
            ViewData["AttenType"] = db.AttendanceTypes.ToList();
            var leaning = db.Learnings.FirstOrDefault(s => s.id == meo.ScheduleDetail.FK_Learning).FK_Class;
            ViewData["user"] = db.Users.Where(s =>s.FK_Class == leaning).ToList();
            return View(meo);
        }
        
        [HttpPost]
        public ActionResult AttendOnline(string[] txtid,string[] txttype,string[] txt_note,string txtatt)
        {
            for(int i=0; i< txtid.Length; i++)
            {
                AttendanceDetail meo = new AttendanceDetail();
                meo.FK_Attendance = int.Parse(txtatt);
                meo.FK_AttendanceDetail_Type = int.Parse(txttype[i]);
                meo.Note = txt_note[i];
                meo.FK_User = txtid[i];
                db.AttendanceDetails.Add(meo);
            }
            db.SaveChanges();
            return RedirectToAction("History");
        }

        public ActionResult Schedule()
        {
            return View();
        }
        public ActionResult Notifi()
        {
            List<Attendance> meo = new List<Attendance>();
            meo = db.Attendances.ToList();
            return View(meo);
        }

        public ActionResult ViewsAttendance(int id)
        {
            Attendance meo = db.Attendances.Find(id);
            ViewData["AttenType"] = db.AttendanceTypes.ToList();
            var leaning = db.Learnings.FirstOrDefault(s => s.id == meo.ScheduleDetail.FK_Learning).FK_Class;
            ViewData["user"] = db.Users.Where(s => s.FK_Class == leaning).ToList();
            return View(meo);
        }
        public ActionResult ReviewAttendace()//Error fix view
        {
            string id_Teacher = Session["id_user"].ToString();
            List<ScheduleDetail> listScheduleDetail = new List<ScheduleDetail>();
            listScheduleDetail = db.ScheduleDetails.Where(s => s.FK_User_GV == id_Teacher).ToList();
            List<int> listShe = new List<int>();
            foreach (var item in listScheduleDetail)
            {
                int meo;
                meo =(int) db.Schedules.Find(item.FK_Schedule).FK_Course;
                listShe.Add(meo);
            }
            List<Course> mon = new List<Course>();//Save List Course's Teacher
            foreach (var item in listShe.Distinct())
            {
                Course su = new Course();
                su = db.Courses.Find(item);
                mon.Add(su);
            }
            TempData["listSchDetail"] = listScheduleDetail;
            return View(mon);
        }
        public ActionResult RevAttenDetail(int id_ScheDetail,int id_Class,int id_subject,int id_course) //getData schedule Detail not need Seesion ID Teacher
        {
            //getData objevct
            List<Attendance> meo = new List<Attendance>();
            meo = db.Attendances.Where(s=>s.FK_ScheduleDetail==id_ScheDetail).ToList();
            var Course_Name = db.Courses.Find(id_course).CourseName;
            var Class_Name = db.Classes.Find(id_Class).ClassName;
            var Subject_Name = db.Subjects.Find(id_subject).SubjectName;
            
            TempData["Course"] = Course_Name;
            TempData["Class"] = Class_Name;
            TempData["Subject"] = Subject_Name;
            return View(meo);
        }
        public ActionResult vieOffline()
        {
            string id_Teacher = Session["id_user"].ToString();
            List<ScheduleDetail> listScheduleDetail = new List<ScheduleDetail>();
            listScheduleDetail = db.ScheduleDetails.Where(s => s.FK_User_GV == id_Teacher).ToList();
            List<int> listShe = new List<int>();
            foreach (var item in listScheduleDetail)
            {
                int meo;
                meo = (int)db.Schedules.Find(item.FK_Schedule).FK_Course;
                listShe.Add(meo);
            }
            List<Course> mon = new List<Course>();//Save List Course's Teacher
            foreach (var item in listShe.Distinct())
            {
                Course su = new Course();
                su = db.Courses.Find(item);
                mon.Add(su);
            }
            TempData["listSchDetail"] = listScheduleDetail;
            return View(mon);
        }
        public ActionResult RevOffline(int id_ScheDetail, int id_Class, int id_subject, int id_course)//Attendance follow with students
        {
            List<Attendance> meo = new List<Attendance>();
            meo = db.Attendances.Where(s => s.FK_ScheduleDetail == id_ScheDetail).ToList();
            var Course_Name = db.Courses.Find(id_course).CourseName;
            var Class_Name = db.Classes.Find(id_Class).ClassName;
            var Subject_Name = db.Subjects.Find(id_subject).SubjectName;
            List<User> listStudent = new List<User>();
            listStudent = db.Users.Where(s => s.FK_Class == id_Class).ToList();
            TempData["ListStudent"] = listStudent;//students
            TempData["Course"] = Course_Name;
            TempData["Class"] = Class_Name;
            TempData["Subject"] = Subject_Name;
            TempData["id_Schedetail"] = id_ScheDetail;
            TempData["id_Course"] = id_course;
            TempData["id_Class"] = id_Class;
            TempData["id_Subject"] = id_subject;
            return View(meo);
        }
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase excelfile,int id_ScheDe,int id_Course,int id_Class,int id_Subject)
        {
            try
            {
                if (excelfile.ContentLength == 0)
                {
                    ViewBag.Error = "Please select a excel file";
                    return Redirect("~/Staff/Teacher/RevOffline/?id_ScheDetail=" + id_ScheDe + "&&id_Class=" + id_Class + "&&id_subject=" + id_Subject + "&&id_course=" + id_Course);
                }
                else
                {
                    if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                    {
                        string path = Server.MapPath("~/Files/" + excelfile.FileName);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        excelfile.SaveAs(path);
                        //getFiles Excel
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;
                        string date = range.Cells[6, 3].Text; //o Call Excel -1
                        string BeginTime = range.Cells[7, 6].Text;
                        string EndTime = range.Cells[7, 7].Text;
                        string Lession = range.Cells[7, 8].Text;
                        string Unit_Lession = range.Cells[7, 9].Text;
                        //Add Enties Attendance
                        Attendance atd_current = new Attendance();
                        atd_current.Date = Convert.ToDateTime(date);
                        atd_current.BeginTime = TimeSpan.Parse(BeginTime);
                        atd_current.EndTime = TimeSpan.Parse(EndTime);
                        atd_current.Lesson = int.Parse(Lession);
                        atd_current.Unit_Lession = Unit_Lession;
                        atd_current.FK_ScheduleDetail = id_ScheDe;
                        db.Attendances.Add(atd_current);
                        db.SaveChanges();
                        //get ID Attendance nearly save
                        int id_AttenNear = db.Attendances.FirstOrDefault(s => s.Date == atd_current.Date && s.BeginTime == atd_current.BeginTime &&
                        s.EndTime == atd_current.EndTime && s.Lesson == atd_current.Lesson && s.Unit_Lession == atd_current.Unit_Lession && s.FK_ScheduleDetail == atd_current.FK_ScheduleDetail
                        ).id;

                        //Add Entities AttendanceDetail
                        for (int row = 8; row <= range.Rows.Count; row++)
                        {
                            if (range.Cells[row, 1].Text != null)
                            {
                                string mssv = range.Cells[row, 1].Text;
                                //string student = range.Cells[row, 2].Text;
                                int Attendance = int.Parse(range.Cells[row, 3].Text.Trim());
                                string Note = range.Cells[row, 4].Text;
                                AttendanceDetail meo = new AttendanceDetail(); //Save SinhVien
                                meo.FK_User = mssv;
                                meo.FK_Attendance = id_AttenNear;
                                meo.FK_AttendanceDetail_Type = Attendance;
                                if (Note != null)
                                {
                                    meo.Note = Note;
                                }
                                else
                                {
                                    meo.Note = " ";
                                }

                                db.AttendanceDetails.Add(meo);
                                db.SaveChanges();
                            }
                            else
                            {
                                break;
                            }

                        }
                        workbook.Close(false);
                        application.Quit();
                        System.IO.File.Delete(path);
                        return Redirect("~/Staff/Teacher/RevOffline/?id_ScheDetail=" + id_ScheDe + "&&id_Class=" + id_Class + "&&id_subject=" + id_Subject + "&&id_course=" + id_Course);
                    }
                    else
                    {
                        ViewBag.Error = "Please select a excel file";
                        return Redirect("~/Staff/Teacher/RevOffline/?id_ScheDetail=" + id_ScheDe + "&&id_Class=" + id_Class + "&&id_subject=" + id_Subject + "&&id_course=" + id_Course);
                    }

                }
            }catch(Exception e)
            {   
                ViewBag.Error = "Please select a excel file";
                return Redirect("~/Staff/Teacher/RevOffline/?id_ScheDetail=" + id_ScheDe + "&&id_Class=" + id_Class + "&&id_subject=" + id_Subject + "&&id_course=" + id_Course);
            }
        }
    }
}