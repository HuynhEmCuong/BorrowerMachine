using Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BorrowerMachine.Controllers
{
  public class DepartController : Controller
  {
    // GET: Depart
    public ActionResult Index()
    {
      return View();

    }
    [HttpPost]
    public bool InsertDepart(string name, string sys)
    {
      var model = new DepartDao().InsertDeprt(name, sys);
      if (model)
        return true;
      else
        return false;
    }
    [HttpPost]
    public bool UpdateDepart(int id, string name, string sys)
    {
      var model = new DepartDao().UpdateDeprt(id, name, sys);
      if (model)
        return true;
      else
        return false;
    }
    [HttpPost]
    public bool DeleteDepart(int id)
    {
      var model = new DepartDao().DeleteDepart(id);
      if (model)
        return true;
      else
        return false;
    }
  }
}
