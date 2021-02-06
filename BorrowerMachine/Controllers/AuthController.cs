using Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BorrowerMachine.Helper;
using Models.EF;
using System.Threading.Tasks;

namespace BorrowerMachine.Controllers
{
  public class AuthController : Controller
  {

    [HttpPost]
    public async Task<JsonResult> Login(string username, string password)
    {
      var user =await new AuthDao().Login(username, password);

      if (user != null)
      {
        CookieUtility<User>.UserCookie = user;
        return Json(user, JsonRequestBehavior.AllowGet);
      }
      else
      {
        return Json(null, JsonRequestBehavior.AllowGet);
      }
    }
  }
}
