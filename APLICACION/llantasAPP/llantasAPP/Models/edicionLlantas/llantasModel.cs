using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Linq;
using System.Web;
using System.Data;

namespace llantasAPP.Models.edicionLlantas
{

    public class llantasInventario
    {
        public string LLANTA { get; set; }
        public string GRUPO { get; set; }
        public int INVENT { get; set; }
        public int VALOR { get; set; }
        public DateTime FECHA { get; set; }
        public int PROVE { get; set; }
        public int FACTURA { get; set; }
        public int FICHA { get; set; }
    }
    public partial class llantas_Edit
    {
        //propiedades de una llanta
        public string LLANTA { get; set; }
        public string GRUPO { get; set; }
        public int POSICION { get; set; }
        public int KINSTALA { get; set; }
        public string FECHAI { get; set; }

    }
    public partial class llantas_Edit
    {
        public string placa { get; set; }
        public int tipoVehiculo { get; set; }
        public int nroEjes { get; set; }
        public int llantasMax { get; set; }
        public int km { get; set; }
        public int llantasCargadas { get; set; }
        public string fechaSistema { get; set; }
        public bool montar { get; set; }
    }//extencion para la informacion del trabajo actual
    public class llantas_Monta
    {
        public string par_vehiculo_e { get; set; }
        public string par_llanta_e { get; set; }
        public string par_grupo_e { get; set; }
        public int par_profi_e { get; set; }
        public int par_profc_e { get; set; }
        public int par_profd_e { get; set; }
        public int par_posicion_e { get; set; }
        public int par_kilomi_e { get; set; }
        public DateTime par_fechai_e { get; set; }
        public int par_presion_e { get; set; }
        public int response { get; set; }
    }
    public class llantas_Rota
    {
        public string par_llanta { get; set; }
        public string par_grupo { get; set; }
        public int par_posicion_ini { get; set; }
        public int par_posicion_fin { get; set; }
        public int response { get; set; }
    }
    public class llantas_Desmonta
    {
        public string par_vehiculo_e { get; set; }
        public string par_llanta_e { get; set; }
        public string par_grupo_e { get; set; }
        public int par_observacion_e { get; set; }
        public int par_kilomi_e { get; set; }
        public DateTime par_fechai_e { get; set; }
        public int par_posicion_e { get; set; }
        public int response { get; set; }
    }
}