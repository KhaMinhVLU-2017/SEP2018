using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;
namespace Sep2018_MVC.Areas.Staff.Controllers
{
    public class GiaovuController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Staff/Giaovu
        public ActionResult Index()
        {
            return View();
        }
        //Hiển thị thời khóa biểu
        public ActionResult Schedule()
        {
            return View();
        }
        //Hiển thị danh sách các môn học
        public ActionResult ListSubject()
        {
            return View();
        }
        //Hiển thị danh sách các lớp học
        [HttpGet]
        public ActionResult ListClass(string course)
        {
            ViewBag.course = course;
            var idcourse = db.Courses.FirstOrDefault(x => x.CourseName.Trim() == course.Trim());
            var listClass = db.Classes.Where(x => x.FK_Course == idcourse.id);
            return View(listClass);
        }
        //Hiển thị các khóa của khoa
        public ActionResult ListCourse()
        {
            List<Course> listCourse = db.Courses.ToList();
            return View(listCourse);
        }
        //Trang index của tính năng statistical
        public ActionResult Statistical()
        {
            return View();
        }
    }
}