using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class GiangvienController : Controller
    {
        SEP_T06Entities1 db = new SEP_T06Entities1();
        // GET: Giangvien
        public ActionResult Index()
        {
            
            return View();
        }
    }
}