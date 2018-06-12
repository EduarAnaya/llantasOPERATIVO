/*
 * llanas OPERATIVO
 * Interfaz llantasOperativo
 * llantasControler
 * marzo 2018
 * Eduar Anaya
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models;
using System.Collections;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace llantasAPP.Controllers
{

    /// <summary>
    /// Controlador llantas
    /// reúne todas las acciones disponibles en las que estén involucradas las llantas
    /// Edicion:{
    /// - Montar
    /// - Desmontar
    /// - Rotar
    /// }
    /// Inventario:{
    /// - Resumen de invenario
    /// }
    /// </summary>

    #region deficnicion de Controlador
    public class llantasController : Controller
    {
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
        //                if (dt.Rows.Count >0)
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

        /**************************nueva version**************************/
        //
        // GET: /llantas/

        /*variables generales*/
        public string fechaSistema = DateTime.Now.ToString("dd/MM/yyyy");
        /*Lista que almacenara las llantas antes de que sean manuladas (para poder ver el antes y el despues)*/
        public static List<llantas_Edit> llantasInicial = new List<llantas_Edit>();
        public static int t_vehiculo = 0;//1:Cabezote;2:Trailer
        public static Stopwatch tiempos = new Stopwatch();


        public ActionResult Inventario()
        {
            tiempos.Start();
            List<llantasInventario> tablaInvenario = new List<llantasInventario>();
            llantasInventario rowinventario = new llantasInventario();
            bdConexion bdcon = new bdConexion();

            try
            {
                DataTable dtInventaio = new DataTable();
                dtInventaio = rowinventario.select_inventario();
                if (dtInventaio.Rows.Count > 0)
                {

                    tablaInvenario = (from DataRow dr in dtInventaio.Rows
                                      select new llantasInventario()
                                          {
                                              LLANTA = dr["LLANTA"].ToString(),
                                              GRUPO = dr["GRUPO"].ToString(),
                                              INVENT = int.Parse(dr["INVENT"].ToString()),
                                              VALOR = int.Parse(dr["VALOR"].ToString()),
                                              FECHA = DateTime.Parse(dr["FECHA"].ToString()),
                                              PROVE = int.Parse(dr["PROVE"].ToString()),
                                              FACTURA = int.Parse(dr["FACTURA"].ToString()),
                                              FICHA = int.Parse(dr["FICHA"].ToString())
                                          }).ToList();
                }
                tiempos.Stop();
                ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                return View(tablaInvenario);
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("ErrorDesc", Ex.Message);
                return View("_Error");
            }
        }

        public ActionResult buscarLLantas()
        {
            ViewBag.title = "Llantas-Buscar";
            return View();
        }

        [HttpPost]
        //[OutputCache(Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "None")]
        public ActionResult editLlantas(string placa, int km)
        {
            tiempos.Start();
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();

                Regex expr_Cabezote = new Regex(@"(^[a-zA-Z]{3}\d{3}$)");
                Regex expr_Trailer = new Regex(@"(^[a-zA-Z]{1}\d{5}$)");

                bool S = expr_Cabezote.IsMatch(placa);

                TempDataDictionary _dataTrabajo = new TempDataDictionary();
                ViewBag.title = "Llantas-Edicion";

                //validar el tipo de vehiculo (Trailer o Cabezote)
                if (expr_Cabezote.IsMatch(placa))//la placa es valida para cabezote
                {
                    #region MONTURA DE LLANTAS
                    t_vehiculo = 2;
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
                    llantas_Edit llanta11 = new llantas_Edit();
                    llantas_Edit llanta12 = new llantas_Edit();
                    llanta1.LLANTA = "123456";
                    llanta1.GRUPO = "255";
                    llanta1.FECHAI = "30/12/2018";
                    llanta1.POSICION = 1;
                    llanta1.SENTIDO = 1;

                    llanta2.LLANTA = "2123456";
                    llanta2.GRUPO = "255";
                    llanta2.FECHAI = "30/12/2018";
                    llanta2.POSICION = 2;
                    llanta2.SENTIDO = 1;

                    llanta3.LLANTA = "3123456";
                    llanta3.GRUPO = "255";
                    llanta3.FECHAI = "30/12/2018";
                    llanta3.POSICION = 3;
                    llanta3.SENTIDO = 1;

                    llanta4.LLANTA = "4123456";
                    llanta4.GRUPO = "255";
                    llanta4.FECHAI = "30/12/2018";
                    llanta4.POSICION = 4;
                    llanta4.SENTIDO = 1;

                    llanta5.LLANTA = "5123456";
                    llanta5.GRUPO = "255";
                    llanta5.FECHAI = "30/12/2018";
                    llanta5.POSICION = 5;
                    llanta5.SENTIDO = 1;

                    llanta6.LLANTA = "6123456";
                    llanta6.GRUPO = "255";
                    llanta6.FECHAI = "30/12/2018";
                    llanta6.POSICION = 6;
                    llanta6.SENTIDO = 1;

                    llanta7.LLANTA = "7123456";
                    llanta7.GRUPO = "255";
                    llanta7.FECHAI = "30/12/2018";
                    llanta7.POSICION = 7;
                    llanta7.SENTIDO = 1;

                    llanta8.LLANTA = "8123456";
                    llanta8.GRUPO = "255";
                    llanta8.FECHAI = "30/12/2018";
                    llanta8.POSICION = 8;
                    llanta8.SENTIDO = 1;

                    llanta9.LLANTA = "9123456";
                    llanta9.GRUPO = "255";
                    llanta9.FECHAI = "30/12/2018";
                    llanta9.POSICION = 9;
                    llanta9.SENTIDO = 1;

                    llanta10.LLANTA = "1012345";
                    llanta10.GRUPO = "255";
                    llanta10.FECHAI = "30/12/2018";
                    llanta10.POSICION = 10;
                    llanta10.SENTIDO = 1;

                    llanta11.LLANTA = "1112345";
                    llanta11.GRUPO = "111";
                    llanta11.FECHAI = "30/12/2018";
                    llanta11.POSICION = 11;
                    llanta11.SENTIDO = 1;

                    llanta12.LLANTA = "1212345";
                    llanta12.GRUPO = "121";
                    llanta12.FECHAI = "30/12/2018";
                    llanta12.POSICION = 12;
                    llanta12.SENTIDO = 1;

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
                    listaLlantas.Add(llanta11);
                    listaLlantas.Add(llanta12);
                    llantasInicial = listaLlantas;

                    ArrayList datos = new ArrayList();//guarda la info de llas llantas en inventario
                    string _llanta = "17496", _grupo = "123";
                    string _llanta2 = "27496", _grupo2 = "456";
                    string[] infollanta = { _llanta, _grupo };
                    string[] infollanta2 = { _llanta2, _grupo2 };
                    datos.Add(infollanta);
                    datos.Add(infollanta2);
                    ViewBag.Countries = datos;
                    #endregion

                    #region RETORNA
                    List<llantas_Edit> infEditL = new List<llantas_Edit>();
                    llantas_Edit infoVehic = new llantas_Edit();
                    /*informacion de edicion*/
                    infoVehic.placa = placa.ToUpper();
                    infoVehic.tipoVehiculo = t_vehiculo;
                    infoVehic.nroEjes = 3;//debe probenir de la consulta
                    infoVehic.km = km;
                    infoVehic.llantasCargadas = listaLlantas.Count;
                    infoVehic.fechaSistema = fechaSistema;
                    infEditL.Add(infoVehic);
                    ViewBag.infoEdit = infEditL;
                    /*
                     * 
                     */
                    tiempos.Stop();
                    ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                    return View(listaLlantas);
                    #endregion
                }
                else
                {
                    tiempos.Stop();
                    ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                    ViewBag.error = 1;
                    ModelState.AddModelError("ErrorDesc", "La placa " + placa + " no existe.");
                    return View("buscarLlantas");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("ErrorDesc", Ex.Message);
                return View("_Error");
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

        //public ActionResult EdicionsLlantas()
        //{
        //    return View();
        //}

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
        public ActionResult llantasRemove
            (string _placa, string _llanta, string _grupo, int _kmMEdida)
        {
            ViewBag._placa = _placa;
            ViewBag._llanta = _llanta;
            ViewBag._grupo = _grupo;
            ViewBag._kmMEdida = _kmMEdida;
            ViewBag._fechaMedida = fechaSistema;
            return View();
        }
        [HttpPost]
        public ActionResult llantasMonta
            (string _placa, string _llanta, string _grupo, int _kmMEdida)
        {
            ViewBag._placa = _placa;
            ViewBag._llanta = _llanta;
            ViewBag._grupo = _grupo;
            ViewBag._kmMEdida = _kmMEdida;
            ViewBag._fechaMedida = fechaSistema;
            return View();
        }

        [HttpPost]
        public ActionResult llantasGuardar(string arrayCamion, string arrayMonta, string arrayDesmonta, string arrayMuestras)
        {
            tiempos.Start();
            try
            {
                #region OB. DIMAMICOS
                //deserelizar los objetos Json enviados desde la vista
                dynamic _arrayCamion = JsonConvert.DeserializeObject(arrayCamion);
                dynamic _arrayMonta = JsonConvert.DeserializeObject(arrayMonta);
                dynamic _arrayDesmonta = JsonConvert.DeserializeObject(arrayDesmonta);
                dynamic _arrayMuestras = JsonConvert.DeserializeObject(arrayMuestras);
                #endregion

                #region LISTAS
                List<llantas_Rota> llantasRotadasL = new List<llantas_Rota>();
                List<llantas_Monta> llantasMontadasL = new List<llantas_Monta>();
                List<llantas_Desmonta> llantasDesmontadasL = new List<llantas_Desmonta>();
                List<llantas_Monta> llantasMuestrasL = new List<llantas_Monta>();
                #endregion

                #region ROTACION LLANTAS
                /*
             * Recorrer la lista inical, y compararla la lista de la vista, para encontrar cambios entre las llantas
             */

                JArray dimCamion = JArray.Parse(arrayCamion);//dimencionar el arrelo de tipo json
                //recorrer el arreglo de menor tamaño para evitar un índice estaba fuera del intervalo.
                List<llantas_Rota> llantasRotaL = new List<llantas_Rota>();
                for (int i = 0; i < dimCamion.Count; i++)
                {
                    string llantaMod = _arrayCamion[i].LLANTA.Value;//LLANTA MODIFICADA
                    int posMod = int.Parse(_arrayCamion[i].POS.Value);//LLANTA MODIFICADA
                    string grupoMod = _arrayCamion[i].GRUPO.Value;
                    bool encontrado;
                    do
                    {
                        for (int j = 0; j < llantasInicial.Count; j++)
                        {
                            string llantaOri = llantasInicial[j].LLANTA;//LLANTA INICIAL
                            int posOri = llantasInicial[j].POSICION;//LLANTA INICIAL
                            string grupoOrig = llantasInicial[j].GRUPO;
                            if (llantaMod == llantaOri)
                            {
                                if (posOri != posMod)
                                {
                                    llantas_Rota _rota = new llantas_Rota();
                                    _rota.par_llanta = llantaOri;
                                    _rota.par_grupo = grupoOrig;
                                    _rota.par_posicion_ini = posOri;
                                    _rota.par_posicion_fin = int.Parse(posMod.ToString());
                                    _rota.response = 1;
                                    llantasRotaL.Add(_rota);
                                }
                                break;
                            }
                        }
                        encontrado = false;

                    } while (encontrado);

                }
                #endregion

                #region MONTADAS
                JArray dimMontadas = JArray.Parse(arrayMonta);
                for (int i = 0; i < dimMontadas.Count; i++)
                {
                    llantas_Monta llantaMondata = new llantas_Monta();
                    llantaMondata.par_vehiculo_e = _arrayMonta[i].par_vehiculo_e.Value;
                    llantaMondata.par_llanta_e = _arrayMonta[i].par_llanta_e;
                    llantaMondata.par_grupo_e = _arrayMonta[i].par_grupo_e;
                    llantaMondata.par_profi_e = _arrayMonta[i].par_profi_e;
                    llantaMondata.par_profc_e = _arrayMonta[i].par_profc_e;
                    llantaMondata.par_profd_e = _arrayMonta[i].par_profd_e;
                    llantaMondata.par_posicion_e = _arrayMonta[i].par_posicion_e;
                    llantaMondata.par_kilomi_e = _arrayMonta[i].par_kilomi_e;
                    llantaMondata.par_fechai_e = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", _arrayMonta[i].par_fechai_e.Value));
                    llantaMondata.par_presion_e = _arrayMonta[i].par_presion_e;
                    llantaMondata.response = 1;
                    /*
                 
                 
                 
                 
                 
                     */
                    llantasMontadasL.Add(llantaMondata);
                }

                #endregion

                #region DESMONTADAS
                JArray dimDesmontadas = JArray.Parse(arrayDesmonta);
                for (int i = 0; i < dimDesmontadas.Count; i++)
                {
                    llantas_Desmonta llantaDesmontada = new llantas_Desmonta();
                    llantaDesmontada.par_vehiculo_e = _arrayDesmonta[i].par_vehiculo_e;
                    llantaDesmontada.par_llanta_e = _arrayDesmonta[i].par_llanta_e;
                    llantaDesmontada.par_grupo_e = _arrayDesmonta[i].par_grupo_e;
                    llantaDesmontada.par_observacion_e = _arrayDesmonta[i].par_observacion_e;
                    llantaDesmontada.par_kilomi_e = _arrayDesmonta[i].par_kilomi_e;
                    llantaDesmontada.par_fechai_e = _arrayDesmonta[i].par_fechai_e;
                    llantaDesmontada.par_posicion_e = _arrayDesmonta[i].par_posicion_e;
                    llantaDesmontada.response = 1;
                    llantasDesmontadasL.Add(llantaDesmontada);
                }
                #endregion

                #region MUESTREOS
                JArray dimMuestreos = JArray.Parse(arrayMuestras);
                for (int i = 0; i < dimMuestreos.Count; i++)
                {
                    llantas_Monta llantaMuestra = new llantas_Monta();
                    llantaMuestra.par_vehiculo_e = _arrayMuestras[i].par_vehiculo_e.Value;
                    llantaMuestra.par_llanta_e = _arrayMuestras[i].par_llanta_e;
                    llantaMuestra.par_grupo_e = _arrayMuestras[i].par_grupo_e;
                    llantaMuestra.par_profi_e = _arrayMuestras[i].par_profi_e;
                    llantaMuestra.par_profc_e = _arrayMuestras[i].par_profc_e;
                    llantaMuestra.par_profd_e = _arrayMuestras[i].par_profd_e;
                    llantaMuestra.par_posicion_e = _arrayMuestras[i].par_posicion_e;
                    llantaMuestra.par_kilomi_e = _arrayMuestras[i].par_kilomi_e;
                    llantaMuestra.par_fechai_e = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", _arrayMuestras[i].par_fechai_e));
                    llantaMuestra.par_presion_e = _arrayMuestras[i].par_presion_e;
                    llantaMuestra.response = 1;
                    llantasMuestrasL.Add(llantaMuestra);
                }
                #endregion

                #region RESULTADOS
                ViewBag.rotadas = llantasRotaL;
                ViewBag.montadas = llantasMontadasL;
                ViewBag.removidas = llantasDesmontadasL;
                ViewBag.muestreos = llantasMuestrasL;

                tiempos.Stop();
                ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                return View();
                #endregion
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("ErrorDesc", Ex.Message);
                return View("_Error");
            }
        }
    }
    #endregion
}