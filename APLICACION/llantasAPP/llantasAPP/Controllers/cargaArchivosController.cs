using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models.cargaArchivos;
using System.IO;
using System.Collections;

namespace llantasAPP.Controllers.cargaArchivos
{
    public class cargaArchivosController : Controller
    {
        //
        // GET: /cargaArchivos/

        public ActionResult Index()
        {
            return View();
        }


        private bool tipoValido(string tipo)
        {
            /*ELENEBTOS PERMITIROS PERMITIDOS:
             * DOCUMENTOS:WORD, PDF,EXCEL
             * IMAGENES:JPG,PNG
             */
            return
                tipo.Equals("application/pdf")
                || tipo.Equals("application/msword")
                || tipo.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                || tipo.Equals("application/vnd.ms-excel")
                || tipo.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                || tipo.Equals("image/jpg")
                || tipo.Equals("image/png");
        }





        [HttpPost]
        public JsonResult cargarArchivo(HttpPostedFileBase archivosSoportesRepara)
        {
            if (archivosSoportesRepara.ContentLength > 0)
            {
                archivos file = new archivos();
                //nombre del archivo a guardar
                file.nombreArchivo = Path.GetFileName(archivosSoportesRepara.FileName);
                file.tamañoArchivo = archivosSoportesRepara.ContentLength;

                //ruta de la carpeta del servidor en la que se guardara
                string ruta = Path.Combine(Server.MapPath(@"/soportes/"), file.nombreArchivo);
                archivosSoportesRepara.SaveAs(ruta);
                return Json(file);
            }
            else
            {
                return Json("error en la carga");
            }

        }

    }
}
