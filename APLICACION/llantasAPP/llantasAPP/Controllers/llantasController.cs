﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models;
using System.Collections;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace llantasAPP.Controllers
{
    public class llantasController : Controller
    {
        //public string fechaSistema = DateTime.Now.Date.ToShortDateString();
        ///*instancia de la clase de conexion a base de datos*/
        //bdConexion bdcon = new bdConexion();
        ///*Lista que almacenara las llantas antes de que sean manuladas (para poder ver el antes y el despues)*/
        //public static List<llantas_Edit> llantasInicial = new List<llantas_Edit>();
        //public static List<llantas_delete> llantasDesmontadas = new List<llantas_delete>();



        //public ActionResult Inventario()
        //{
        //    List<llantasInventario> tablaInvenario = new List<llantasInventario>();
        //    llantasInventario rowinventario = new llantasInventario();
        //    /*
        //     * 
        //     * 
        //     * 
        //     * 
        //     */

        //    tablaInvenario.Add(rowinventario);

        //    return View(tablaInvenario);
        //}

        //public ActionResult EdicionsLlantas()
        //{
        //    return View();
        //}

        //public ActionResult RecircularLlantas()
        //{
        //    return View();
        //}

        //public ActionResult ReencaucheLlantas()
        //{
        //    return View();
        //}

        //public ActionResult ReparacionLlantas()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public JsonResult cargarllantass(int _tipoVehiculo, string _placa)
        //{
        //    ArrayList datos = new ArrayList();
        //    //List<llantas_model> llantaL = new List<llantas_model>();
        //    DataTable dt = new DataTable();
        //    DataTable dtv = new DataTable();
        //    try
        //    {
        //        bool preguntar = bdcon.Conectar();
        //        if (preguntar)
        //        {
        //            string tipoV;
        //            string nroEjes;
        //            /*
        //             ENTONCE:
        //             * _tipoVehiculo =1: ES UN CABEZOTE
        //             * _tipoVehiculo =2: ES UN TRAILER
        //             */
        //            bool vExiste = false;//EL VEHICULO EXISTE 1:0
        //            string[] datosVehiculos;//TIPO Y NUMERO DE EJES


        //            if (_tipoVehiculo == 1)
        //            {
        //                string[] tablas = { "VEHICULOS" };
        //                string[] campos = { "VEHI_PLACA_CH" };
        //                string condicion = "VEHI_PLACA_CH='" + _placa + "'";
        //                dt = bdcon.select(campos, tablas, condicion);
        //                if (dt.Rows.Count == 1)
        //                {
        //                    vExiste = true;
        //                    datosVehiculos = new string[] { tipoV = "C", nroEjes = "3" };
        //                    datos.Add(datosVehiculos);
        //                }
        //                else
        //                {
        //                    datos.Add(0);//VEHICULO NO EXISTE
        //                }
        //            }
        //            else if (_tipoVehiculo == 2)
        //            {
        //                string[] tablas = { "TRAILERS" };
        //                string[] campos = { "TRAI_PLACA_CH", "TRAI_NOEJES_NB" };
        //                string condicion = "TRAI_PLACA_CH='" + _placa + "'";
        //                dt = new DataTable();
        //                dt = bdcon.select(campos, tablas, condicion);
        //                if (dt.Rows.Count == 1)
        //                {
        //                    vExiste = true;
        //                    foreach (DataRow row in dt.Rows)
        //                    {
        //                        tipoV = "T";
        //                        nroEjes = row.ItemArray[1].ToString();
        //                        datosVehiculos = new string[] { tipoV, nroEjes };
        //                        datos.Add(datosVehiculos);
        //                    }
        //                }
        //                else
        //                {
        //                    datos.Add(0);//VEHICULO NO EXISTE
        //                }
        //            }

        //            if (vExiste)
        //            {
        //                llantasInicial.Clear();//vaciamos la lista de llantas inicial
        //                string[] tablasv = { "LLANTAS" };
        //                //string[] camposv = { "LLANTA", "GRUPO", "KINSTALA", "VEHICULO", "POSICION" };
        //                string[] camposv = { "*" };
        //                string condicionv = "VEHICULO='" + _placa + "'";
        //                dtv = bdcon.select(camposv, tablasv, condicionv);
        //                bdcon.Desconectar();
        //                if (dtv.Rows.Count > 0)
        //                {
        //                    foreach (DataRow row in dtv.Rows)
        //                    {
        //                        llantas_Edit llanta1 = new llantas_Edit();
        //                        foreach (DataColumn column in dtv.Columns)
        //                        {
        //                            llanta1.LLANTA = row[0].ToString();
        //                            llanta1.GRUPO = row[1].ToString();
        //                            llanta1.VALOR = (int)row[2];
        //                            llanta1.FECHA = Convert.ToDateTime(row[3]).ToString("dd/mm/yyyy");
        //                            llanta1.PROVEE = (int)row[4];
        //                            llanta1.FACTURA = (int)row[5];
        //                            llanta1.FICHA = (int)row[6];
        //                            llanta1.NEUMA = (short)row[7];
        //                            llanta1.VALORRN = (int)row[8];
        //                            llanta1.PROTEC = (short)row[9];
        //                            llanta1.VALORP = (int)row[10];
        //                            llanta1.VEHICULO = row[11].ToString();
        //                            llanta1.POSICION = (short)row[12];
        //                            llanta1.KINSTALA = (int)row[13];
        //                            llanta1.FECHAI = Convert.ToDateTime(row[14]).ToString("dd/mm/yyyy");


        //                        }
        //                        llantasInicial.Add(llanta1);
        //                    }
        //                    datos.Add(llantasInicial);
        //                }
        //                else
        //                {
        //                    datos.Add(0);//SI ELVEHICUO EXISTE PERO DEVUELE 0 LLANTAS
        //                }
        //            }
        //            else
        //            {
        //                bdcon.Desconectar();
        //                datos.Add(0);//SI ELVEHICUO NO EXISTE DEVUELE 0 LLANTAS COSULTADAS
        //            }

        //        }

        //        return Json(datos);
        //    }
        //    catch (Exception)
        //    {
        //        return Json("ERROR");
        //    }
        //}

        //[HttpPost]
        //public ActionResult llantasDisponibles()
        //{
        //    List<string[,]> llantasD = new List<string[,]>();

        //    try
        //    {
        //        bool testCon = bdcon.Conectar();
        //        if (testCon)
        //        {
        //            string[] tablas = { "INVENTARIO" };
        //            string[] campos = { "LLANTA", "GRUPO" };
        //            string condicion = "";

        //            DataTable dt = new DataTable();
        //            dt = bdcon.select(campos, tablas, condicion);
        //            bdcon.Desconectar();

        //            foreach (DataRow row in dt.Rows)
        //            {
        //                string[,] datos = new string[1, 2];

        //                string z = row.ItemArray[0].ToString();
        //                string k = row.ItemArray[1].ToString();
        //                datos[0, 0] = z;
        //                datos[0, 1] = k;

        //                llantasD.Add(datos);
        //            }

        //        }
        //        return Json(llantasD);
        //    }
        //    catch
        //    {
        //        return Json("");
        //    }
        //}

        //[HttpPost]
        //public ActionResult llantasGuardar(
        //    string _placa, string _fechaInstala, int _kmIntsla, string llantasMontadas,
        //    string llantasImportadas, string llantasDesmontadas)
        //{

        //    dynamic llantasRemovidas = JsonConvert.DeserializeObject(llantasDesmontadas);

        //    for (int i = 0; i < llantasRemovidas.Count; i++)
        //    {

        //    }

        //    return Json(1);
        //}


        //[HttpPost]
        //public ActionResult removerllantas(string _placa, string _llanta, string _grupo, string _kmMEdida)
        //{
        //    ViewBag._Placa = _placa;
        //    ViewBag._Llanta = _llanta;
        //    ViewBag._Grupo = _grupo;
        //    ViewBag._KmMEdida = _kmMEdida;
        //    ViewBag._fechaMedida = fechaSistema;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult removerllantasPost
        //    (string par_vehiculo, string par_llanta, string par_grupo, string par_profi,
        //    string par_profc, string par_profd, int par_posicion, int par_kilomi, int par_presion)
        //{
        //    llantas_delete llantaRemove = new llantas_delete();
        //    llantaRemove.par_vehiculo = par_vehiculo;
        //    llantaRemove.par_llanta = par_llanta;
        //    llantaRemove.par_grupo = par_grupo;
        //    llantaRemove.par_profi = par_profi;
        //    llantaRemove.par_profc = par_profc;
        //    llantaRemove.par_profd = par_profd;
        //    llantaRemove.par_posicion = par_posicion;
        //    llantaRemove.par_kilomi = par_kilomi;
        //    llantaRemove.par_presion = par_presion;
        //    llantasDesmontadas.Add(llantaRemove);
        //    return Json("Ok");
        //}
        /**************************nueva version**************************/
        //
        // GET: /llantas/

        /*variables generales*/
        public string fechaSistema = DateTime.Now.Date.ToShortDateString();
        public string _placaActual;
        public int _km = 0;


        public ActionResult Index()
        {
            ViewBag.Title = "Llantas-Home";
            return View();
        }
        public ActionResult buscarLLantas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult editLlantas(string placa, int km)
        {
            Regex expr_Cabezote = new Regex(@"(^[a-zA-Z]{3}\d{3}$)");
            Regex expr_Trailer = new Regex(@"(^[a-zA-Z]{1}\d{5}$)");
            int t_vehiculo;//1:Cabezote;2:Trailer
            bool S = expr_Cabezote.IsMatch(placa);

            TempDataDictionary _dataTrabajo = new TempDataDictionary();

            //validar el tipo de vehiculo (Trailer o Cabezote)
            if (expr_Cabezote.IsMatch(placa))//la placa es valida para cabezote
            {
                t_vehiculo = 1;
                //consultar a la base da datos la placa del cabezote (retorna 10 llantas)
                List<llantas_Edit> listaLlantas = new List<llantas_Edit>();
                llantas_Edit llanta1 = new llantas_Edit();
                llantas_Edit llanta2 = new llantas_Edit();
                llantas_Edit llanta3 = new llantas_Edit();
                llantas_Edit llanta4 = new llantas_Edit();
                llantas_Edit llanta5 = new llantas_Edit();
                llantas_Edit llanta6 = new llantas_Edit();
                llantas_Edit llanta7 = new llantas_Edit();
                llantas_Edit llanta8 = new llantas_Edit();
                llantas_Edit llanta9 = new llantas_Edit();
                llantas_Edit llanta10 = new llantas_Edit();
                llanta1.LLANTA = "10255";
                llanta2.LLANTA = "10255";
                llanta3.LLANTA = "10255";
                llanta4.LLANTA = "10255";
                llanta5.LLANTA = "10255";
                llanta6.LLANTA = "10255";
                llanta7.LLANTA = "10255";
                llanta8.LLANTA = "10255";
                llanta9.LLANTA = "10255";
                llanta10.LLANTA = "10255";
                listaLlantas.Add(llanta1);
                listaLlantas.Add(llanta2);
                listaLlantas.Add(llanta3);
                listaLlantas.Add(llanta4);
                listaLlantas.Add(llanta5);
                listaLlantas.Add(llanta6);
                listaLlantas.Add(llanta7);
                listaLlantas.Add(llanta8);
                listaLlantas.Add(llanta9);
                listaLlantas.Add(llanta10);

                TempData["_llanta"] = placa;
                TempData["_TV"] = "CABEZOTE";
                TempData["_nEjes"] = 3;//debe probenir de la consulta
                TempData["_km"] = km;
                TempData["_nLlantas"] = listaLlantas.Count;
                TempData["_fecActual"] = fechaSistema;

                //n.Add("TV", 0);
                //n.Add("ejes", 3);
                //TempData = n;
                ViewBag.TV = 1;




                string[] llantass = { "alfa", "alba", "hasta" };
                ViewBag.llantas = llantass;
                //ViewData["llantas"] = llantass;

                string[] Countries = { "India", "US", "Nepal", "alfa", "alba", "hasta",
                                     "India", "US", "Nepal", "alfa", "alba", "hasta","India",
                                     "US", "Nepal", "alfa", "alba", "hasta","India", "US", "Nepal", "alfa", "alba", "hasta"};
                ViewBag.Countries = Countries;
                return View(listaLlantas);
            }
            else
            {
                TempData["Error"] = "La placa " + placa + " no existe.";
                return View("buscarLlantas");
            }
            //if (expr_Trailer.IsMatch(placa))//la placa es valida para trailer
            //{
            //    t_vehiculo = 2;
            //}


            //if (km == 10)//la placa no existe
            //{
            //    TempData["Error"] = "La placa " + placa + " no existe.";
            //    return View("buscarLlantas");
            //}

            //else
            //{
            //    List<llantas_Edit> listaLlantas = new List<llantas_Edit>();
            //    llantas_Edit llanta = new llantas_Edit();
            //    llanta.LLANTA = "10255";
            //    listaLlantas.Add(llanta);
            //    return View(listaLlantas);
            //}



        }

    }
}
