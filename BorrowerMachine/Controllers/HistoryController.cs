using Models.DAOs;
using Models.EF;
using Models.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BorrowerMachine.Controllers
{
  public class HistoryController : Controller
  {
    private DBContext db = new DBContext();
    // GET: History
    public ActionResult Index()
    {
      return View();
    }

    public JsonResult GetAllHistory(int idCate, int page)
    {
      return Json(new BorrowerDao().GetAllHistory(idCate, page), JsonRequestBehavior.AllowGet);

    }

    public ActionResult ExportListHistory()
    {
      int idCate = 0;
      var data = new BorrowerDao().ExportHistory(idCate);

      var result = data.Select(x => new
      {
        x.DepartName,
        x.CateDeviceName,
        x.DevcieName,
        x.EmployName,
        x.DateBorrow,
        x.DateReturn
      }).ToList();
      var excelPackage = new ExcelPackage();
      // Add Sheet vào file Excel
      excelPackage.Workbook.Worksheets.Add("Report Sheet");

      var workSheet = excelPackage.Workbook.Worksheets[1];
      // Ð? data vào Excel file
      workSheet.Cells[2, 1].LoadFromCollection(result);
      workSheet.Cells[1, 1].Value = "Bộ phận";
      workSheet.Cells[1, 2].Value = "Danh mục";
      workSheet.Cells[1, 3].Value = "Tên thiết bị";
      workSheet.Cells[1, 4].Value = "Người mượn";
      workSheet.Cells[1, 5].Value = "Ngày mượn";
      workSheet.Cells[1, 6].Value = "Ngày trả";

      using (var range = workSheet.Cells[1, 1, 1, 6])
      {
        // Set PatternType
        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        // Set color  Background
        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
        // Canh gi?a cho các text
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        // Set Font cho text  trong Range hi?n t?i
        range.Style.Font.SetFromFont(new System.Drawing.Font("Arial", 14));
        //Set Border
        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        //// Set color Border
        //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
        // Set color text
        range.Style.Font.Color.SetColor(Color.White);
      }
      var rowData = result.Count() + 1;

      //Format Date
      workSheet.Cells[2, 5, rowData, 6].Style.Numberformat.Format = "dd-mm-yyyy";

      // fix auto fit column
      workSheet.Cells[1, 1, rowData, 6].AutoFitColumns();
      using (var range = workSheet.Cells[1, 1, rowData, 6])
      {
        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        range.Style.Font.Size = 12;
      }
      excelPackage.Save();

      // T?o buffer memory strean d? h?ng file excel
      var buffer = excelPackage.Stream as MemoryStream;
      // Ðây là content Type dành cho file excel
      Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      Response.AddHeader("Content-Disposition", "attachment; filename=History_Borrower" + DateTime.Now.ToString("-ddMMyy_hhmmss") + ".xlsx");
      // Luu file excel c?a chúng ta nhu 1 m?ng byte d? tr? v? response
      Response.BinaryWrite(buffer.ToArray());
      // Send t?t c? ouput bytes v? phía clients
      Response.Flush();
      Response.End();
      return View();
    }
  }
}
