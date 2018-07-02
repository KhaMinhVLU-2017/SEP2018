using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class SinhvienController : Controller
    {
        sep21t16Entities db = new sep21t16Entities();
        // GET: Sinhvien
        public ActionResult Index()
        {
            if(Session["username"] == null) { return RedirectToAction("Index", "Account"); }
            //Lay id lop cua user
            string s = Session["lop"].ToString();
            int n = int.Parse(s);
            var sj = db.lichhocs.Where( x=> x.lop_id == n);
            return View(sj);
        }
        public ActionResult Edit(string mssv)
        {

            var user = db.sinhviens.SingleOrDefault(x => x.sv_mssv == mssv);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(sinhvien sv)
        {

            var user = db.sinhviens.FirstOrDefault(x => x.sv_mssv == sv.sv_mssv);
            user.sv_bday = sv.sv_bday;
            user.sv_email = sv.sv_email;
            user.sv_name = sv.sv_name;
            user.sv_sdt = sv.sv_sdt;
            user.lp_id = sv.lp_id;
            user.sv_gtinh = sv.sv_gtinh;
           
            db.SaveChanges();
            return RedirectToAction("Account","Sinhvien");
        }
        [HttpGet]
        public ActionResult Attendance(string idmonhoc)
        {
            if (Session["id"] == null) { return RedirectToAction("Index", "Account"); }

            string s = Session["id"].ToString();
            var diemdanh = db.diemdanhs.Where(x => x.monhoc.mh_name.Trim() == idmonhoc && x.sv_id == s);
            if (diemdanh != null)

            {
                ViewBag.monhoc = idmonhoc;
                ViewBag.tre = diemdanh.Count(x => x.stt_id == 3);
                ViewBag.co = diemdanh.Count(x => x.stt_id == 4);
                ViewBag.phep = diemdanh.Count(x => x.stt_id == 2);
                ViewBag.vang = diemdanh.Count(x => x.stt_id == 1);
                return View(diemdanh);
            }
            else
            {
                ViewBag.msg = "Chưa đi học một buổi nào luôn á _-" ;
                return View(diemdanh);
            }
        }
        public ActionResult Sinhvien() => View();

        public ActionResult Account()
        {

            if (Session["username"] == null) { return RedirectToAction("Index", "Account"); }
            string s = Session["id"].ToString().Trim();
            var person = db.sinhviens.FirstOrDefault(x => x.sv_mssv.Trim() == s);
            if(person == null)
            {
                ViewBag.msg = "Tài khoản chưa được kích hoạt. Vui lòng đăng kí ở phòng đào tạo !";
            }
            return View(person);
        }
        
        
       
    }
}