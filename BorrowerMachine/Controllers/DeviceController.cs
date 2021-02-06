using Models.DAOs;
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
  public class DeviceController : Controller
  {
    // GET: Device
    public ActionResult Index()
    {
      return View();
    }
    [HttpPost]
    public JsonResult GetAllDevice(int IdCate)
    {
      return Json(new DeviceDao().GetAllDevice(IdCate), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public bool InsertDevice(string name, int idCate, int quantity, string nameCreate)
    {
      var model = new DeviceDao().InsertDevice(name, idCate, quantity, nameCreate);
      if (model)
        return true;
      else
        return false;
    }

    [HttpPost]
    public bool UpdateDevice(int id, string name, int idCate, int quantity, string nameUpdate)
    {
      var model = new DeviceDao().UpdateDevice(id, name, idCate, quantity, nameUpdate);
      if (model)
        return true;
      else
        return false;
    }

    [HttpPost]
    public bool DeleteDevice(int id)
    {
      var model = new DeviceDao().DeleteDevice(id);
      if (model)
        return true;
      else
        return false;
    }

    public ActionResult ExportDevice()
    {
      int idCate = 0;
      var data = new DeviceDao().GetAllDevice(idCate);

      var result = data.Select(x => new
      {
        x.NameCateDevice,
        x.NameDevice,
        x.Quantity,
      }).ToList();

      var excelPackage = new ExcelPackage();
      // Add Sheet vào file Excel
      excelPackage.Workbook.Worksheets.Add("Report Sheet");

      var workSheet = excelPackage.Workbook.Worksheets[1];
      workSheet.Cells[2, 1].LoadFromCollection(result);
      workSheet.Cells[1, 1].Value = "Tên thiết bị ";
      workSheet.Cells[1, 2].Value = "Danh mục";
      workSheet.Cells[1, 3].Value = "Số lượng trong kho";
      using (var range = workSheet.Cells[1, 1, 1, 3])
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
      workSheet.Cells[1, 1, rowData, 3].AutoFitColumns();
      using (var range = workSheet.Cells[1, 1, rowData, 3])
      {
        range.Style.Font.Size = 12;
        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      }
        excelPackage.Save();

      var buffer = excelPackage.Stream as MemoryStream;
      Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      Response.AddHeader("Content-Disposition", "attachment; filename=List_Device" + DateTime.Now.ToString("-ddMMyy_hhmmss") + ".xlsx");
      // Luu file excel c?a chúng ta nhu 1 m?ng byte d? tr? v? response
      Response.BinaryWrite(buffer.ToArray());
      // Send t?t c? ouput bytes v? phía clients
      Response.Flush();
      Response.End();
      return View();
    }
  }
}
