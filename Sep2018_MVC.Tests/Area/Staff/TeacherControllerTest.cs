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
        public void SemesterMeo() //Test API Semester of Studdents
        {
            //Desire
            List<object> JsonDesire = new List<object>();
            foreach(var item in db.Courses.ToList())
            {
                JsonDesire.Add(new { id_sm = item.Semester.id,smName = item.Semester.SemesterName});
            }
            //Arrange
            TeacherController controller = new TeacherController();
            //Act
            List<object> Acctual = new List<object>();
            foreach(var item in db.Courses.ToList())
            {
                JsonResult result = controller.SemesterMeo(item.id) as JsonResult;
              
                Acctual.Add(result.Data);
            }
            //Assert
            Assert.AreEqual(JsonDesire.Count(),Acctual.Count());
        }
        [TestMethod]
        public void ClassMeo() //Test API Class Of Students
        {
            //Arrange
            List<object> JsonDesire = new List<object>();

            foreach (var item in db.Semesters.ToList())
            {
                foreach(var Subitem in db.Courses.Where(s => s.FK_Semester == item.id))
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
        public void Subject()
        {
            //Arrange
            List<object> JsonDesire = new List<object>();

            foreach(var item in db.Semesters.ToList())
            {
                foreach(var subitem in db.Courses.Where(s => s.FK_Semester == item.id))
                {
                    foreach(var xClass  in db.Classes.Where(s => s.FK_Course == subitem.id))
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
                foreach(var XClass  in db.Classes.Where(s => s.FK_Course == item.id))
                {
                    JsonResult result = controller.Subject(XClass.id,item.id) as JsonResult;

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
        
    }

}
