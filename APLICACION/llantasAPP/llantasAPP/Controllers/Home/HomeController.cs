using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models.Account;
using llantasAPP.seguridad;

namespace llantasAPP.Controllers.Home
{
    [CustomAuthorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.Title = "Llantas-Home";
            return View();
        }

    }
}
