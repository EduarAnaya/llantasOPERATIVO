using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models;

namespace llantasAPP.Controllers
{
    public class llantasController : Controller
    {
        //
        // GET: /llantas/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventario()
        {
            List<llantasInventario> tablaInvenario = new List<llantasInventario>();
            llantasInventario rowinventario = new llantasInventario();
            /*
             * 
             * 
             * 
             * 
             */

            tablaInvenario.Add(rowinventario);

            return View(tablaInvenario);
        }
    }
}
