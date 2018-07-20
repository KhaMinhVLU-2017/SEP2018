using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sep2018_MVC.ExcelHander;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Areas.Staff.Controllers
{
    public class ExportController : Controller
    {
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        // GET: Staff/Export
        public void Excel(int id_course,int id_Class,int id_subject)
        {
            StudentExcel excel = new StudentExcel();
            Response.ClearContent();
            Response.BinaryWrite(excel.GenerateExcel(id_course, id_Class, id_subject));
            Response.AddHeader("content-disposition", "attachment; filename=attendanceVlu.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Flush();
            Response.End();
        }
    }
}