using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sep2018_MVC.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Sep2018_MVC.ExcelHander
{
    public class StudentExcel
    {
        ExcelRange cell;
        ExcelFill fill;
        Border border;
        SEP_2018_T6Entities1 db = new SEP_2018_T6Entities1();
        public byte[] GenerateExcel(int id_course,int id_Class,int id_subject)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "JudasFate";
                excelPackage.Workbook.Properties.Title = "Attendance";
                var sheet = excelPackage.Workbook.Worksheets.Add("Students Excel");
                sheet.Name = "Student Excel Report";
                sheet.Column(2).Width = 30;
                sheet.Column(3).Width = 20;
                sheet.Column(4).Width = 20;
                sheet.Column(6).Width = 15;
                sheet.Column(7).Width = 15;
                sheet.Column(8).Width = 15;
                sheet.Column(9).Width = 15;
                #region Report Header
                sheet.Cells[2, 3, 3, 9].Merge = true;
                cell = sheet.Cells[2, 3];
                cell.Value = "ListAttendance for Student:"+ db.Subjects.Find(id_subject).SubjectName +" - "+ db.Courses.Find(id_course).CourseName;
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 18;
                cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                #endregion

                #region Header Date
                cell = sheet.Cells[7, 2];
                cell.Value = "Date";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header DateCurrent
                cell = sheet.Cells[7, 3];
                cell.Value = "01/27/2018";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header BeginTime
                cell = sheet.Cells[7, 6];
                cell.Value ="BeginTime";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header BeginTime Content
                cell = sheet.Cells[8, 6];
                cell.Value = "07:00:00";
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header EndTime
                cell = sheet.Cells[7, 7];
                cell.Value = "EndTime";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header EndTimeContent
                cell = sheet.Cells[8, 7];
                cell.Value = "09:30:00";
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header Lession
                cell = sheet.Cells[7, 8];
                cell.Value = "Lession";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header LessionContent
                cell = sheet.Cells[8, 8];
                cell.Value = "3";
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                #region Header UnitLession
                cell = sheet.Cells[7, 9];
                cell.Value = "Unit_Lession";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header UnitLession
                cell = sheet.Cells[8, 9];
                cell.Value = "Tiết";
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion


                #region Header DataType
                cell = sheet.Cells[11, 6];
                cell.Value = "DataType";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header NameType
                cell = sheet.Cells[11, 7];
                cell.Value = "NameType";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                int count = 0;
                foreach(var item in db.AttendanceTypes.ToList()){
                    #region Header DataTypeContent
                    cell = sheet.Cells[12+count, 6];
                    cell.Value = item.id;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    #region Header NameTypeContent
                    cell = sheet.Cells[12+ count, 7];
                    cell.Value = item.TypeName;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    count++;
                }

                #region Header MSSV
                cell = sheet.Cells[8,1];
                cell.Value = "MSSV";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header Student
                cell = sheet.Cells[8, 2];
                cell.Value = "Student";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header Attendance
                cell = sheet.Cells[8, 3];
                cell.Value = "Attendance";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion
                #region Header Note
                cell = sheet.Cells[8, 4];
                cell.Value = "Note";
                cell.Style.Font.Bold = true;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                #endregion

                int counp = 0;
                foreach(var item in db.Users.Where(s => s.FK_Class == id_Class).ToList())
                {

                    #region Header MSSVContent
                    cell = sheet.Cells[9+ counp, 1];
                    cell.Value = item.username;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    #region Header StudentContent
                    cell = sheet.Cells[9+counp, 2];
                    cell.Value = db.People.FirstOrDefault(s=>s.MS==item.username).Name;
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    #region Header AttendanceContent
                    cell = sheet.Cells[9+ counp, 3];
                    cell.Value = "";
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    #region Header NoteContent
                    cell = sheet.Cells[9+ counp, 4];
                    cell.Value = "";
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.White);
                    border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    #endregion
                    counp++;
                }
                return excelPackage.GetAsByteArray();
            }
        }
    }
}