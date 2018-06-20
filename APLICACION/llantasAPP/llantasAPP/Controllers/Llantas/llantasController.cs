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
using llantasAPP.Models.edicionLlantas;


namespace llantasAPP.Controllers
{

    /// <summary>
    /// Controlador llantas
    /// reúne todas las acciones disponibles en las que estén involucradas las llantas
    /// Edicion:{
    /// - Montar
    /// - Desmontar
    /// - Rotar
    /// - Muestras
    /// }
    /// Inventario:{
    /// - Resumen de invenario
    /// }
    /// </summary>

    #region deficnicion de Controlador
    public class llantasController : Controller
    {
        //
        // GET: /llantas/

        /*variables generales*/
        public string fechaSistema = DateTime.Now.ToString("yyyy-MM-dd");
        /*Lista que almacenara las llantas antes de que sean manuladas (para poder ver el antes y el despues)*/
        public static List<llantas_Edit> llantasInicial = new List<llantas_Edit>();
        public procLlantas _llantasProc = new procLlantas();
        public static Stopwatch tiempos = new Stopwatch();

        public ActionResult Inventario()
        {
            tiempos.Start();
            List<llantasInventario> tablaInvenario = new List<llantasInventario>();
            bdConexion bdcon = new bdConexion();

            try
            {
                DataTable dtInventaio = new DataTable();
                dtInventaio = _llantasProc.select_inventario();
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
        [OutputCache(Duration = 0, Location = OutputCacheLocation.None)]
        public ActionResult editLlantas(string placa, int km)
        {
            string _placa = placa.ToUpper();
            tiempos.Start();
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();

                llantas_Edit llantaInterfaz = new llantas_Edit();

                #region VALIDAR VEHICULO
                /*
                 * 1.Identificar el tipo de vechiculo Cabezote/Trailer segun las caracteristicas de la placa
                 * 2.Validar si la Placa ingresada existe.
                 * 
                 */
                Regex expr_Cabezote = new Regex(@"(^[a-zA-Z]{3}\d{3}$)");
                Regex expr_Trailer = new Regex(@"(^[a-zA-Z]{1}\d{5}$)");

                int t_vehiculo = 0;//1:Cabezote;2:Trailer
                int valido = 0;
                if (expr_Cabezote.IsMatch(_placa))//la placa es valida para cabezote
                {
                    t_vehiculo = 1;
                    valido = _llantasProc.valVehiculo(_placa, t_vehiculo).Rows.Count;
                }
                if (expr_Trailer.IsMatch(_placa))//la placa es valida para trailer
                {
                    t_vehiculo = 2;
                    valido = _llantasProc.valVehiculo(_placa, t_vehiculo).Rows.Count;
                }
                #endregion

                #region MONTURA DE LLANTAS
                if (valido != 0)//el vehiculo existe
                {
                    DataTable dt = new DataTable();
                    List<llantas_Edit> listaLlantas = new List<llantas_Edit>();
                    dt = _llantasProc.FDB_DATOS_PLACA_2_0(_placa);
                    int NroEjes = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                    string tipo = dt.Rows[0].ItemArray[1].ToString();
                    if (tipo != "")
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            // NroEjes = int.Parse(item["NOEJES"].ToString());
                            llantas_Edit llantaItem = new llantas_Edit();
                            llantaItem.LLANTA = item["NOLLANTA"].ToString();
                            llantaItem.GRUPO = item["GRUPO"].ToString();
                            llantaItem.FECHAI = item["FECINSTALA"].ToString();
                            llantaItem.POSICION = int.Parse(item["POSICION"].ToString());
                            llantaItem.KINSTALA = int.Parse(item["KINSTALA"].ToString());
                            llantaItem.montar = true;
                            listaLlantas.Add(llantaItem);
                        }
                        llantasInicial = listaLlantas;
                    }
                #endregion

                    #region LLANTAS DISPONIBLES
                    ArrayList _listaDisponible = new ArrayList();//guarda la info de llas llantas en inventario
                    DataTable _listadisponibles = _llantasProc.llantasDisponibles();

                    foreach (DataRow llanta in _listadisponibles.Rows)
                    {
                        string[] infollanta = { llanta["LLANTA"].ToString(), llanta["GRUPO"].ToString() };
                        _listaDisponible.Add(infollanta);
                    }
                    ViewBag.llantasDinponibles = _listaDisponible;
                    #endregion

                    #region RETORNA
                    List<llantas_Edit> infEditL = new List<llantas_Edit>();
                    llantas_Edit infoVehic = new llantas_Edit();
                    /*informacion de edicion*/
                    ViewBag.title = "Llantas-Edicion";
                    infoVehic.placa = _placa;
                    infoVehic.tipoVehiculo = t_vehiculo;
                    infoVehic.nroEjes = NroEjes;
                    infoVehic.km = km;
                    infoVehic.fechaSistema = fechaSistema;
                    switch (NroEjes)
                    {
                        case 1:
                            infoVehic.llantasMax = 4;
                            break;
                        case 2:
                            infoVehic.llantasMax = 8;
                            break;
                        case 3:
                            if (t_vehiculo == 1)
                            {
                                infoVehic.llantasMax = 10;
                            }
                            else
                            {
                                infoVehic.llantasMax = 12;
                            }
                            break;
                    }
                    if (listaLlantas.Count > infoVehic.llantasMax)
                    {
                        tiempos.Stop();
                        ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                        string _errorDisplay = "LL-002: Se ha encontrado inconsistencias en la relación de llantas montadas " +
                        "y la capacidad real del vehículo, por favor informar a sistemas.";
                        ModelState.AddModelError("ErrorLlantas", _errorDisplay);
                        return View("buscarLlantas");
                    }

                    /*
                 * Dentro de la consulta puede que hayan posiciones vacías,
                 * para poder montar todas las llantas consultadas en la posición correspondiste
                 * se debe conocer el número máximo de llantas que podría tener un vehículo, (esto se puede saber según el numero de ejes),
                 * luego recorrer cada posible posición y compararlo con la llantas consultadas,
                 * se valida si en la posición se monta o no una llanta (la llanta consultada) de no coincidir se asume que esa posición quedara vacía.
                 */
                    List<llantas_Edit> llantasMontadas = new List<llantas_Edit>();
                    for (int i = 1; i < infoVehic.llantasMax + 1; i++)//recorrer todas la posibles posiciones
                    {
                        llantas_Edit llantaMonta = new llantas_Edit();
                        bool Salir = true;//false
                        bool existe = false;
                        if (listaLlantas.Count > 0)
                        {
                            while (Salir == true)
                            {
                                foreach (llantas_Edit item in listaLlantas)//recorrer las llantas consultadas
                                {
                                    if (i == item.POSICION)
                                    {
                                        llantasMontadas.Add(item);
                                        Salir = false;
                                        existe = true;
                                        break;
                                    }
                                    else
                                    {
                                        Salir = false;
                                        existe = false;
                                    }
                                }
                                break;
                            }
                        }

                        if (existe == false)//La posicion en el eje I no tiene una llanta montada, por ende se crea una caja vacia (para poder montar )
                        {
                            llantaMonta.POSICION = i;
                            llantaMonta.montar = false;
                            llantasMontadas.Add(llantaMonta);
                        }

                    }

                    infoVehic.llantasCargadas = listaLlantas.Count;//las consultadas
                    infEditL.Add(infoVehic);
                    ViewBag.infoEdit = infEditL;

                    /*
                     * 
                     */
                    tiempos.Stop();
                    ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                    //return View(listaLlantas);
                    return View(llantasMontadas);
                    #endregion
                }
                else
                {
                    tiempos.Stop();
                    ViewBag.tiempoProceso = tiempos.Elapsed.TotalSeconds;
                    string _errorDisplay = "LL-000: La placa " + _placa + " no existe.";
                    ModelState.AddModelError("ErrorLlantas", _errorDisplay);
                    return View("buscarLlantas");
                }
            }

            catch (Exception Ex)
            {
                ModelState.AddModelError("ErrorDesc", Ex.StackTrace);
                return View("_Error");
            }

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
        public ActionResult llantasRemove(string _placa, string _llanta, string _grupo, int _kmMEdida)
        {
            ViewBag._placa = _placa;
            ViewBag._llanta = _llanta;
            ViewBag._grupo = _grupo;
            ViewBag._kmMEdida = _kmMEdida;
            ViewBag._fechaMedida = fechaSistema;
            return View();
        }

        [HttpPost]
        public ActionResult llantasMonta(string _placa, string _llanta, string _grupo, int _kmMEdida)
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
                    llantaMondata.response = _llantasProc.PDB_MONTARLLANTA_2_0(llantaMondata.par_vehiculo_e, llantaMondata.par_llanta_e, llantaMondata.par_grupo_e, llantaMondata.par_profi_e, llantaMondata.par_profc_e, llantaMondata.par_profd_e, llantaMondata.par_posicion_e, llantaMondata.par_kilomi_e, llantaMondata.par_fechai_e, llantaMondata.par_presion_e);
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
                    //proceso de desmonte en la base de datos
                    llantaDesmontada.response = _llantasProc.PDB_DESMONTARLLANTA_2_0(llantaDesmontada.par_vehiculo_e, llantaDesmontada.par_llanta_e, llantaDesmontada.par_grupo_e, llantaDesmontada.par_observacion_e, llantaDesmontada.par_kilomi_e, llantaDesmontada.par_fechai_e);
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