using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models;
using System.Collections;
using System.Data;
using Newtonsoft.Json;

namespace llantasAPP.Controllers
{
    public class llantasController : Controller
    {
        /*instancia de la clase de conexion a base de datos*/
        bdConexion bdcon = new bdConexion();
        /*Lista que almacenara las llantas antes de que sean manuladas (para poder ver el antes y el despues)*/
        public static List<llantas_Edit> llantaL = new List<llantas_Edit>();

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

        public ActionResult EdicionLlantas()
        {
            return View();
        }

        public ActionResult RecircularLlantas()
        {
            return View();
        }
        
        public ActionResult ReencaucheLlantas()
        {
            return View();
        }
        
        public ActionResult ReparacionLlantas()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult cargarllantas(int _tipoVehiculo, string _placa)
        {
            ArrayList datos = new ArrayList();
            //List<llantas_model> llantaL = new List<llantas_model>();
            DataTable dt = new DataTable();
            DataTable dtv = new DataTable();
            try
            {
                bool preguntar = bdcon.Conectar();
                if (preguntar)
                {
                    string tipoV;
                    string nroEjes;
                    /*
                     ENTONCE:
                     * _tipoVehiculo =1: ES UN CABEZOTE
                     * _tipoVehiculo =2: ES UN TRAILER
                     */
                    bool vExiste = false;//EL VEHICULO EXISTE 1:0
                    string[] datosVehiculos;//TIPO Y NUMERO DE EJES


                    if (_tipoVehiculo == 1)
                    {
                        string[] tablas = { "VEHICULOS" };
                        string[] campos = { "VEHI_PLACA_CH" };
                        string condicion = "VEHI_PLACA_CH='" + _placa + "'";
                        dt = bdcon.select(campos, tablas, condicion);
                        if (dt.Rows.Count == 1)
                        {
                            vExiste = true;
                            datosVehiculos = new string[] { tipoV = "C", nroEjes = "3" };
                            datos.Add(datosVehiculos);
                        }
                        else
                        {
                            datos.Add(0);//VEHICULO NO EXISTE
                        }
                    }
                    else if (_tipoVehiculo == 2)
                    {
                        string[] tablas = { "TRAILERS" };
                        string[] campos = { "TRAI_PLACA_CH", "TRAI_NOEJES_NB" };
                        string condicion = "TRAI_PLACA_CH='" + _placa + "'";
                        dt = new DataTable();
                        dt = bdcon.select(campos, tablas, condicion);
                        if (dt.Rows.Count == 1)
                        {
                            vExiste = true;
                            foreach (DataRow row in dt.Rows)
                            {
                                tipoV = "T";
                                nroEjes = row.ItemArray[1].ToString();
                                datosVehiculos = new string[] { tipoV, nroEjes };
                                datos.Add(datosVehiculos);
                            }
                        }
                        else
                        {
                            datos.Add(0);//VEHICULO NO EXISTE
                        }
                    }

                    if (vExiste)
                    {
                        llantaL.Clear();//vaciamos la lista de llantas inicial
                        string[] tablasv = { "LLANTAS" };
                        //string[] camposv = { "LLANTA", "GRUPO", "KINSTALA", "VEHICULO", "POSICION" };
                        string[] camposv = { "*" };
                        string condicionv = "VEHICULO='" + _placa + "'";
                        dtv = bdcon.select(camposv, tablasv, condicionv);
                        bdcon.Desconectar();
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                llantas_Edit llanta1 = new llantas_Edit();
                                foreach (DataColumn column in dtv.Columns)
                                {
                                    llanta1.LLANTA = row[0].ToString();
                                    llanta1.GRUPO = row[1].ToString();
                                    llanta1.VALOR = (int)row[2];
                                    llanta1.FECHA = Convert.ToDateTime(row[3]).ToString("dd/mm/yyyy");
                                    llanta1.PROVEE = (int)row[4];
                                    llanta1.FACTURA = (int)row[5];
                                    llanta1.FICHA = (int)row[6];
                                    llanta1.NEUMA = (short)row[7];
                                    llanta1.VALORRN = (int)row[8];
                                    llanta1.PROTEC = (short)row[9];
                                    llanta1.VALORP = (int)row[10];
                                    llanta1.VEHICULO = row[11].ToString();
                                    llanta1.POSICION = (short)row[12];
                                    llanta1.KINSTALA = (int)row[13];
                                    llanta1.FECHAI = Convert.ToDateTime(row[14]).ToString("dd/mm/yyyy");


                                }
                                llantaL.Add(llanta1);
                            }
                            datos.Add(llantaL);
                        }
                        else
                        {
                            datos.Add(0);//SI ELVEHICUO EXISTE PERO DEVUELE 0 LLANTAS
                        }
                    }
                    else
                    {
                        bdcon.Desconectar();
                        datos.Add(0);//SI ELVEHICUO NO EXISTE DEVUELE 0 LLANTAS COSULTADAS
                    }

                }

                return Json(datos);
            }
            catch (Exception)
            {
                return Json("ERROR");
            }
        }

        [HttpPost]
        public ActionResult llantasDisponibles()
        {
            List<string[,]> llantasD = new List<string[,]>();

            try
            {
                bool testCon = bdcon.Conectar();
                if (testCon)
                {
                    string[] tablas = { "INVENTARIO" };
                    string[] campos = { "LLANTA", "GRUPO" };
                    string condicion = "";

                    DataTable dt = new DataTable();
                    dt = bdcon.select(campos, tablas, condicion);
                    bdcon.Desconectar();

                    foreach (DataRow row in dt.Rows)
                    {
                        string[,] datos = new string[1, 2];

                        string z = row.ItemArray[0].ToString();
                        string k = row.ItemArray[1].ToString();
                        datos[0, 0] = z;
                        datos[0, 1] = k;

                        llantasD.Add(datos);
                    }

                }
                return Json(llantasD);
            }
            catch
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult llantasGuardar(
            string _placa, string _fechaInstala, int _kmIntsla, string llantasMontadas,
            string llantasImportadas, string llantasDesmontadas)
        {

            dynamic llantasRemovidas = JsonConvert.DeserializeObject(llantasDesmontadas);

            for (int i = 0; i < llantasRemovidas.Count; i++)
            {

            }

            return Json(1);
        }


        [HttpPost]
        public ActionResult removerllantas
            (string par_vehiculo, string par_llanta, string par_grupo, string par_profi,
            string par_profc, string par_profd, int par_posicion, int par_kilomi, int par_presion)
        {

            return View();
        }

    }
}
