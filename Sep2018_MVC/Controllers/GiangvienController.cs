using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    [Authorize]
    public class GiangvienController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Giangvien
        public ActionResult Index()
        {            
            return View();
        }
    }
}