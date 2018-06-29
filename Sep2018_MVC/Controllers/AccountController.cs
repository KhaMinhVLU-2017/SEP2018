using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class AccountController : Controller
    {
        SEP_T06Entities1 db = new SEP_T06Entities1();
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email, string pw)
        {
            //kiem tra xem tai khoan dang nhap thuoc role gi
            var usersvien = db.users.FirstOrDefault(x => x.user_name.Trim() == email);
            var userGiangVien = db.usergiaoviens.FirstOrDefault(x => x.usgv_username.Trim() == email);
            var userGiaoVu = db.usergiaovus.FirstOrDefault(x => x.usgvu_username.Trim() == email);
            //neu role la sinh vien
            if(usersvien != null)
            {
                string s1 = pw.Trim();
                if (usersvien.user_pw.Trim().Equals(s1))
                {
                    Session["role"] = usersvien.role;
                    Session["username"] = usersvien.sinhvien.sv_name.Trim();
                    Session["lop"] = usersvien.sinhvien.lop.lop_id;
                    Session["id"] = usersvien.user_name.Trim();
                    return RedirectToAction("Sinhvien", "Sinhvien");
                    
                }else
                {
                    ViewBag.msg = "Quên luôn mật khẩu mới ác :v ";
                }
            }
            else
            {
                //neu role la giang vien
                if (userGiangVien != null)
                {
                    string s1 = pw.Trim();
                    if (userGiangVien.usgv_pw.Trim().Equals(s1))
                    {
                        Session["username"] = userGiangVien.giaovien.gv_ten.Trim();
                        Session["role"] = userGiangVien.role_id;
                        return RedirectToAction("Index", "Giangvien");
                    }
                    else
                    {
                        ViewBag.msg = "Quên luôn mật khẩu mới ác :v ";
                    }
                }
                else
                {
                    //neu role la giao vu
                    if (userGiaoVu != null)
                    {
                        string s1 = pw.Trim();
                        if (userGiaoVu.usgvu_pw.Trim().Equals(s1))
                        {
                            Session["username"] = userGiaoVu.giaovu.gvu_ten.Trim();
                            Session["role"] = userGiaoVu.role_id;
                            return RedirectToAction("Index", "Giaovu");
                        }
                        else
                        {
                            
                            ViewBag.msg = "Quên luôn mật khẩu mới ác :v ";
                        }
                    }
                    else
                    {
                        //Neu tai khoang khong ton tai
                        ViewBag.msg = "Tài khoản cũng quên cho được hè _- !!!" ;
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}