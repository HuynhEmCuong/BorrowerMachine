using Models.DAOs;
using Models.DTOs;
using Models.EF;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorrowerMachine.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public JsonResult GetListCategory()
    {
      return Json(new DeviceDao().GetAllCategory(), JsonRequestBehavior.AllowGet);
    }

    public JsonResult GetListBorrower(int idCategory)
    {
      return Json(new BorrowerDao().GetAllBorrower(idCategory), JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public JsonResult GetDevice(int id)
    {
      return Json(new DeviceDao().GetDevice(id), JsonRequestBehavior.AllowGet);
    }

    public JsonResult GetListDepart()
    {
      return Json(new DepartDao().GetAllDepert(), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public string UpdateBorrower(BorrowerReturnDto data)
    {
      var model = new BorrowerDao().UpdateBorrower(data);
      if (model == true)
        return "OK";
      else
        return "Fail";
    }

    [HttpPost]
    public bool InsertBorrower(List<BorrowerDto> data)
    {
      new EmployDao().InsertAndUpdate(data);
      var borrower = new BorrowerDao().InsertBorrower(data);
      if (borrower)
      {
        return true;
      }
      else
        return false;
    }

    [HttpGet]
    public JsonResult SearchEmploy(string keyword)
    {
      var t = new EmployDao().SearchEmploy(keyword);
      return Json(new EmployDao().SearchEmploy(keyword), JsonRequestBehavior.AllowGet);
    }

    public ActionResult ExportBorrower()
    {
      int idCate = 0;
      var data = new BorrowerDao().GetAllBorrower(idCate);

      var result = data.Select(x => new
      {
        x.DepartName,
        x.EmployName,
        x.DevcieName,
        x.Quantity,
        x.DateBorrow
      }).ToList();

      var excelPackage = new ExcelPackage();
      // Add Sheet vào file Excel
      excelPackage.Workbook.Worksheets.Add("Report Sheet");

      var workSheet = excelPackage.Workbook.Worksheets[1];
      workSheet.Cells[2, 1].LoadFromCollection(result);
      workSheet.Cells[1, 1].Value = "Bộ Phận ";
      workSheet.Cells[1, 2].Value = "Người mượn";
      workSheet.Cells[1, 3].Value = "Tên thiết bị";
      workSheet.Cells[1, 4].Value = "Số lượng mượn";
      workSheet.Cells[1, 5].Value = "Ngày mượn";
      using (var range = workSheet.Cells[1, 1, 1, 5])
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
        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        ////// Set color Border
        //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
        // Set color text
        range.Style.Font.Color.SetColor(Color.White);
      }
      var rowData = result.Count() + 1;
      workSheet.Cells[1, 1, rowData, 5].AutoFitColumns();
      using (var range = workSheet.Cells[1, 1, rowData, 5])
      {
        range.Style.Font.Size = 12;
        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      }
      workSheet.Cells[2, 5, rowData, 5].Style.Numberformat.Format = "dd-mm-yyyy";
      excelPackage.Save();

      var buffer = excelPackage.Stream as MemoryStream;
      Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      Response.AddHeader("Content-Disposition", "attachment; filename=List_Borrower" + DateTime.Now.ToString("-ddMMyy_hhmmss") + ".xlsx");
      // Luu file excel c?a chúng ta nhu 1 m?ng byte d? tr? v? response
      Response.BinaryWrite(buffer.ToArray());
      // Send t?t c? ouput bytes v? phía clients
      Response.Flush();
      Response.End();

      return View();
    }
  }
}
