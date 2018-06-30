using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sep2018_MVC;
using Sep2018_MVC.Models;
using Sep2018_MVC.Areas.Staff.Controllers;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace Sep2018_MVC.Tests.Area.Staff
{
    [TestClass]
    public class TeacherControllerTest
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        [TestMethod]
        public void CreateAccount()
        {
            //Arange
            TeacherController controller = new TeacherController();
            //Act
            ViewResult result = controller.CreateAccount() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void API_SemesterMeo() //Test API Semester of Studdents
        {
            //Desire
            List<object> JsonDesire = new List<object>();
            foreach (var item in db.Courses.ToList())
            {
                JsonDesire.Add(new { id_sm = item.Semester.id, smName = item.Semester.SemesterName });
            }
            //Arrange
            TeacherController controller = new TeacherController();
            //Act
            List<object> Acctual = new List<object>();
            foreach (var item in db.Courses.ToList())
            {
                JsonResult result = controller.SemesterMeo(item.id) as JsonResult;

                Acctual.Add(result.Data);
            }
            //Assert
            Assert.AreEqual(JsonDesire.Count(), Acctual.Count());
        }
        [TestMethod]
        public void API_ClassMeo() //Test API Class Of Students
        {
            //Arrange
            List<object> JsonDesire = new List<object>();

            foreach (var item in db.Semesters.ToList())
            {
                foreach (var Subitem in db.Courses.Where(s => s.FK_Semester == item.id))
                {
                    List<object> ConCon = new List<object>();
                    foreach (var SClass in db.Classes.Where(s => s.FK_Course == Subitem.id))
                    {
                        //Desire

                        ConCon.Add(new { id_class = SClass.id, name_Class = SClass.ClassName });
                    }
                    JsonDesire.Add(ConCon);
                }
            }
            //Act
            TeacherController controller = new TeacherController();
            List<object> Acctual = new List<object>();
            foreach (var item in db.Courses.ToList())
            {
                JsonResult result = controller.ClassMeo(item.FK_Semester, item.id) as JsonResult;

                Acctual.Add(result.Data);
            }
            //Assert
            Assert.AreEqual(JsonDesire.Count(), Acctual.Count());
        }
        [TestMethod]
        public void API_Subject()
        {
            //Arrange
            List<object> JsonDesire = new List<object>();

            foreach (var item in db.Semesters.ToList())
            {
                foreach (var subitem in db.Courses.Where(s => s.FK_Semester == item.id))
                {
                    foreach (var xClass in db.Classes.Where(s => s.FK_Course == subitem.id))
                    {
                        List<object> SubCon = new List<object>();
                        foreach (var xSubject in db.Learnings.Where(d => d.FK_Class == xClass.id))
                        {
                            SubCon.Add(new { id_subject = xSubject.Subject.id, subName = xSubject.Subject.SubjectName });
                        }
                        JsonDesire.Add(SubCon);
                    }
                }
            }
            //Actual
            TeacherController controller = new TeacherController();
            List<object> Acctual = new List<object>();
            foreach (var item in db.Courses.ToList())
            {
                foreach (var XClass in db.Classes.Where(s => s.FK_Course == item.id))
                {
                    JsonResult result = controller.Subject(XClass.id, item.id) as JsonResult;

                    Acctual.Add(result.Data);
                }

            }
            //Assert
            Assert.AreEqual(JsonDesire.Count(), Acctual.Count());
        }
        [TestMethod]
        public void InformationAccount()//Test Redirect of Function
        {
            //Arrange
            TeacherController controller = new TeacherController();
            //Acctual
            ActionResult result = controller.InformationAccount();
            //Asert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void History()   //Test History Attendance of Teacher
        {
            //Desire
            var dataHistory = db.Attendances.ToList();
            //Arrange
            TeacherController controller = new TeacherController();
            //Acctual
            var actual = ((ViewResult)controller.History()).Model as List<Attendance>;
            //Assert
            if (dataHistory.Count == actual.Count)
            {
                CheckData(dataHistory, actual);
            }
            else
            {
                Assert.AreEqual(dataHistory.Count, actual.Count);
            }
        }
        /*
         * You should Check once row for Easy Handing
         */
        public void CheckData(List<Attendance> Desire, List<Attendance> Actual)
        {
            int count = 0;
            foreach (var item in Actual)
            {
                Assert.AreEqual(Desire[count].id, item.id);
                Assert.AreEqual(Desire[count].Lesson, item.Lesson);
                Assert.AreEqual(Desire[count].Unit_Lession, item.Unit_Lession);
                Assert.AreEqual(Desire[count].FK_ScheduleDetail, item.FK_ScheduleDetail);
                count++;
            }
        }
        [TestMethod]
        public void CreateSection()
        {
            //Desire
            List<Course> DesireData = db.Courses.ToList();
            //Arrange
            TeacherController controller = new TeacherController();
            //Actual
            var Actual = ((ViewResult)controller.CreateSection()).Model as List<Course>;
            //Asert
            if (DesireData.Count == Actual.Count)//Check Count Data
            {
                CheckDataCourse(DesireData, Actual);
            }
            else
            {
                Assert.AreEqual(DesireData.Count, Actual.Count);
            }
        }
        /*
         * You should Check once row for Easy Handing
         */
        public void CheckDataCourse(List<Course> Desire, List<Course> Actual)
        {
            int count = 0;
            foreach (var item in Actual)
            {
                Assert.AreEqual(Desire[count].id, item.id);
                Assert.AreEqual(Desire[count].CourseName, item.CourseName);
                Assert.AreEqual(Desire[count].Semester.id, item.Semester.id);
                Assert.AreEqual(Desire[count].ShoolYearEnd, item.ShoolYearEnd);
                Assert.AreEqual(Desire[count].FK_Semester, item.FK_Semester);
                count++;
            }
        }
        [TestMethod]
        public void Schedule()
        {
            //Arrange
            TeacherController controller = new TeacherController();
            //Actual
            var result = controller.Schedule();
            //Assert Check Type Of Action
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Notifi()
        {
            //Arrange
            TeacherController controller = new TeacherController();
            //Actual
            ViewResult result = controller.Notifi() as ViewResult;
            //Assert Check Type Of Action
            Assert.IsNotNull(result); // check have null
        }
        [TestMethod]
        public void EditAttend()//Test Edit Attendance when you inside HIstory
        {
            //Desire
            List<Attendance> DataDesire = db.Attendances.ToList();
            //Arrange
            TeacherController controller = new TeacherController();
            //Acttual
            foreach (var item in DataDesire)
            {
                var result = ((ViewResult)controller.EditAttend(item.id)).Model as Attendance;
                //Assert
                Assert.AreEqual(item.id, result.id);
                Assert.AreEqual(item.Lesson, result.Lesson);
                Assert.AreEqual(item.ScheduleDetail.id, result.ScheduleDetail.id);
                Assert.AreEqual(item.Unit_Lession, result.Unit_Lession);
                Assert.AreEqual(item.FK_ScheduleDetail, result.FK_ScheduleDetail);
            }
        }
        [TestMethod]
        public void API_ScheDetail()
        {
            //Arrange
            int id_Course = 3;  // K20
            int id_Semester = 1;    //HK1
            int id_Class = 9;   //Class T2
            int id_Subject = 8;     //Quan Tri Doanh Nghiep
            int id_learning = db.Learnings.FirstOrDefault(s => s.FK_Subject == id_Subject && s.FK_Semester == id_Semester && s.FK_Class == id_Class).id;
            TeacherController controller = new TeacherController();

            //Actual
            JsonResult result = controller.ScheDetail(id_Subject, id_Course, id_Semester, id_Class) as JsonResult;


            //Assert
            Assert.IsNotNull(result.Data);
        }
        [TestMethod]
        public void CheckOnline()
        {
            //Arrange
      
            int id_Course = 4;          //Khoa 21
            int id_Semester = 1;        //HK1
            int id_Class = 12;          //T2
            int id_Subject = 3;         //Software Requirement
            int txt_Lesson = 3;         //Tiet Hoc
            DateTime txt_time = Convert.ToDateTime("01/07/2018");   //Date
            TimeSpan timeFrom = TimeSpan.Parse("3:00");
            TimeSpan timeTo = TimeSpan.Parse("5:00");
            int id_SCheduleDetail = 39;     //Software Requirment 7:30 --> 9:30
            TeacherController controller = new TeacherController();
            //Actual
            var result = ((ViewResult)controller.CheckOnline(id_Course, id_SCheduleDetail, txt_Lesson, id_Semester, id_Class,id_Subject, txt_time, timeFrom, timeTo)).Model ;
            //Assert
            Assert.IsNotNull(result);
            //DeleteModel
            int maxCount = db.Attendances.Max(s => s.id);
            Attendance model = db.Attendances.Find(maxCount);
            db.Attendances.Remove(model);
            db.SaveChanges();
        }
        [TestMethod]
        public void ViewsAttendance()//Test Edit Attendance when you inside HIstory
        {
            //Desire
            List<Attendance> DataDesire = db.Attendances.ToList();
            //Arrange
            TeacherController controller = new TeacherController();
            //Acttual
            foreach (var item in DataDesire)
            {
                var result = ((ViewResult)controller.EditAttend(item.id)).Model as Attendance;
                //Assert
                Assert.AreEqual(item.id, result.id);
                Assert.AreEqual(item.Lesson, result.Lesson);
                Assert.AreEqual(item.ScheduleDetail.id, result.ScheduleDetail.id);
                Assert.AreEqual(item.Unit_Lession, result.Unit_Lession);
                Assert.AreEqual(item.FK_ScheduleDetail, result.FK_ScheduleDetail);
            }
        }
    }
    public class Result
    {
        public bool success;
        public string error;
    }

}
