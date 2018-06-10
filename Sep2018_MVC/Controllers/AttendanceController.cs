using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Sep2018_MVC;
using Sep2018_MVC.Models;

namespace Sep2018_MVC.Controllers
{
    public class AttendanceController : Controller
    {
        //Index Attendance
        SEP_2018_T6Entities2 db = new SEP_2018_T6Entities2();
        public ActionResult Index()
        {
            var list = db.AttendanceDetails.ToList();
            return View(list);
        }
        public ActionResult importfile()
        {

            return View();
        }
        // POST: Attendance import file
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Files/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    //fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //Neu file co duoi xls
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //Neu file co duoi xlsx
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Tao ket noi voi Excel
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Files/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }
                for (int i = 4; i < ds.Tables[0].Rows.Count; i++)
                {
                       /* string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);
                        string query = "Insert into AttendanceDetail(FK_Attendance,FK_User,FK_AttendanceDetail_Type) Values('"
                            + ds.Tables[0].Rows[i][4].ToString()
                        + "','" 
                        + ds.Tables[0].Rows[i][1].ToString()
                        + "','"
                        + ds.Tables[0].Rows[i][7].ToString() + "')";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();*/
                    var atd = new AttendanceDetail();
                    //atd.FK_User = ds.Tables[0].Rows[i][0].ToString();
                   // string s = ds.Tables[0].Rows[i][0].ToString();
                   // if (s != string.Empty)
                   //     s = "t1411";
                    atd.FK_User = "t1413";
                    int x = -1;
                    switch(ds.Tables[0].Rows[i][3].ToString())
                    {
                        case "x":
                            x = 1;
                            break;
                        case "t":
                            x = 2;
                            break;
                        case "p":
                            x = 3;
                            break;
                        case "v":
                            x = 4;
                            break;
                        case "":
                            break;
                    }
                    atd.FK_AttendanceDetail_Type = x;
                    atd.FK_Attendance = DateTime.Now.Day;
                    db.AttendanceDetails.Add(atd);
                    db.SaveChanges();
                }
                
            }
            return RedirectToAction("Index","Attendance");
        }
    }
}