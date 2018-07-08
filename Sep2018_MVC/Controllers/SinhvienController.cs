using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    [Authorize(Roles ="Sinh Vien")]
    public class SinhvienController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Sinhvien
        public ActionResult Index()
        {
            string id = Session["id_user"].ToString();
            List<Learning> listLearning = new List<Learning>();
            int? Mate_Class = db.Users.FirstOrDefault(s=>s.username==id).FK_Class;
            listLearning = db.Learnings.Where(s => s.FK_Class == Mate_Class).ToList();
            ViewData["list_Learning"] = listLearning;
            return View();
        }
        //[HttpGet]
        //public ActionResult Index(string subject)
        //{
        //    var sj = db.Subjects.ToList();
        //    return View(sj);
        //}

    }
}