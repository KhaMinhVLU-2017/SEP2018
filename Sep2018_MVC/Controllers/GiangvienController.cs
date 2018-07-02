using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Sep2018_MVC.Controllers
{
    public class GiangvienController : Controller
    {
        sep21t16Entities db = new sep21t16Entities();
        // GET: Giangvien
       
        public ActionResult Index()
        {

            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        //view chọn điểm danh online hay offline
        public ActionResult chose()
        {
            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            return View();
        }
        //view chinrh sua thong tin Giang vien
        public ActionResult Edit(string msgv)
        {

            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            var user = db.giaoviens.SingleOrDefault(x => x.gv_ms == msgv);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(giaovien sv)
        {

            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            var user = db.giaoviens.FirstOrDefault(x => x.gv_ms == sv.gv_ms);
          
            user.gv_email = sv.gv_email;
            user.gv_ten = sv.gv_ten;
            user.gv_sdt = sv.gv_sdt;
            user.gv_gioitinh = sv.gv_gioitinh;
            user.gv_diachi = sv.gv_diachi;
            db.SaveChanges();
            return RedirectToAction("Account", "Giangvien");
        }
        //view hiện ra danh sách các sinh viên có lớp là "idmonhoc"
        [HttpGet]
        public ActionResult Attendance(string idmonhoc)
        {
            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            var sinhvien = db.sinhviens.Where(x => x.lop.lop_name == idmonhoc);
            ViewBag.lop = idmonhoc;
            //tạo session
            var sess = new session();
            string smh = Session["monhoc"].ToString();
            var mh = db.monhocs.FirstOrDefault(s => s.mh_name == smh);
            sess.mh_id = mh.mh_id;
            string ss = Session["id"].ToString();
            var usergv = db.usergiaoviens.FirstOrDefault(x => x.usgv_username == ss);
            sess.us_id = usergv.usgv_id;
            var sss = db.sessions.Max(x => x.ss_id);
            var lichhocc = db.lichhocs.Where(x => x.monhoc.mh_name == idmonhoc);
            var lop = db.lops.FirstOrDefault(x => x.lop_name.Trim() == idmonhoc.Trim());
            sess.lop_id = lop.lop_id;
            sess.ss_create = DateTime.Now.Date;
            Session["session"] = sss;
            db.sessions.Add(sess);
            db.SaveChanges();
            return View(sinhvien);
            
        }
        //view hiện ra những lớp học môn học này, môn học = "idmonhoc" 
        [HttpGet]
        public ActionResult listlop (string idmonhoc)
        {
            if (Session["id"] == null) { return RedirectToAction("Index", "Home"); }
            Session["monhoc"] = idmonhoc;
            var lophocs = db.lichhocs.Where(x => x.monhoc.mh_name == idmonhoc);

            return View(lophocs);
        }
        //Hiện ra những môn mà giảng viên dạy
        public ActionResult Giangvien()
        {
            if (Session["username"] == null) { return RedirectToAction("Index", "Home"); }
            //Lay id gv cua user
            string s = Session["id"].ToString();
            var sj = db.lichdays.Where(x => x.gv_id == s);
            return View(sj);
        }

        //Thông tin của giảng viên
        public ActionResult Account()
        {

            if (Session["username"] == null) { return RedirectToAction("Index", "Home"); }
            string s = Session["id"].ToString().Trim();
            var person = db.giaoviens.FirstOrDefault(x => x.gv_ms.Trim() == s);
            if (person == null)
            {
                ViewBag.msg = "Tài khoản chưa được kích hoạt. Vui lòng đăng kí ở phòng đào tạo !";
            }
            return View(person);
        }
        //Điểm danh online
        [HttpPost]
        public ActionResult online(List<string> stt_id, List<string> mssv)
        {
           
            
                string s = Session["monhoc"].ToString();
                var diemdanh = new diemdanh();
                var idmh = db.monhocs.Single(x => x.mh_name == s);
                var sta = db.status.Single(x => x.stt_id == 1);
                diemdanh.mh_id = 1;
                diemdanh.stt_id = sta.stt_id;
                string ss = Session["id"].ToString();
                diemdanh.sv_id = "T150793";
                string sss = Session["session"].ToString();
                int n = int.Parse(sss);
                diemdanh.ss_id = n;
                db.diemdanhs.Add(diemdanh);
                db.SaveChanges();
            
           
            return RedirectToAction("success");
        }
        //tạo session khi điểm danh
      

       
        //view import file
        public ActionResult offline()
        {
            if (Session["username"] == null) { return RedirectToAction("Index", "Home"); }
                
            return View();
        }
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            
            if (Request.Files["file"].ContentLength == 0)
            {
                ViewBag.Error = "Vui lòng chọn file";
                return View("offline");
            }
            else
            {
                if(file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Files/" + file.FileName);
                    //luu file
                   // file.SaveAs(path);
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path, CorruptLoad: true);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<diemdanh> diemdanhs = new List<diemdanh>();
                    for(int col = 0; col <= 40; col++)
                    {
#pragma warning disable CS0162 // Unreachable code detected
                        for (int row = 0; row <= 50; row++)
#pragma warning restore CS0162 // Unreachable code detected
                        {
                            ViewBag.scc = ((Excel.Range)range.Cells[0][0]).Text;

                            string s = ((Excel.Range)range.Cells[9][2]).Text;
                            if ( s.Trim() == "MÃ SV")
                            {
                               
                                for(int irow = row+1 ; irow <= 50; irow++)
                                {
                                    for(int jcol = col+6; jcol <= 40; jcol++)
                                    {
                                        diemdanh dd = new diemdanh();
                                        dd.sv_id = ((Excel.Range)range.Cells[irow][col]).Text;
                                        int n = int.Parse(Session["session"].ToString());
                                        dd.ss_id = n;
                                        int nMonhoc = int.Parse(Session["monhoc"].ToString());
                                        dd.mh_id = nMonhoc;
                                        string comment = ((Excel.Range)range.Cells[row][jcol]).Comment.Text().Trim() ;
                                        
                                        if (comment != null) {
                                            var time = db.thoikhoabieux.FirstOrDefault(x => x.tkb_date.ToString().Trim() == comment);
                                            if (time != null)
                                            {
                                                
                                                dd.tkb_id = time.tkb_id;
                                                string sstatus = ((Excel.Range)range.Cells[irow][jcol]).Text().Trim();
                                                switch (sstatus)
                                                {
                                                    case "0":
                                                        sstatus = "1";
                                                        break;
                                                    case "1":
                                                        sstatus = "4";
                                                        break;
                                                    case "P":
                                                        sstatus = "2";
                                                        break;
                                                    case "T":
                                                        sstatus = "3";
                                                        break;
                                                }
                                                dd.stt_id = int.Parse(sstatus);
                                                db.diemdanhs.Add(dd);
                                                db.SaveChanges();
                                            }
                                        }
                                        

                                    }
                                }
                                
                            }
                            break;
                           
                            
                        }
                    }




                    return View("success");
                }
                else
                {
                    ViewBag.Error = "Vui lòng chọn file Excel <br/>";

                    return View("offline");
                }
            }
        }

        //view thông báo import thành công
        public ActionResult success()
        {
            
            return View();
        }
    }
}