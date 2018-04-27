using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace llantasAPP.Models
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
    public class llantas_Edit
    {
        //propiedades de una llanta
        public string LLANTA { get; set; }
        public string GRUPO { get; set; }
        public int VALOR { get; set; }
        public string FECHA { get; set; }
        public int PROVEE { get; set; }
        public int FACTURA { get; set; }
        public int FICHA { get; set; }
        public int NEUMA { get; set; }
        public int VALORRN { get; set; }
        public int PROTEC { get; set; }
        public int VALORP { get; set; }
        public string VEHICULO { get; set; }
        public int POSICION { get; set; }
        public int SENTIDO { get; set; }
        public int KINSTALA { get; set; }
        public string FECHAI { get; set; }
    }

    public class llantas_delete
    {
        public string par_vehiculo { get; set; }
        public string par_llanta { get; set; }
        public string par_grupo { get; set; }
        public string par_profi { get; set; }
        public string par_profc { get; set; }
        public string par_profd { get; set; }
        public int par_posicion { get; set; }
        public int par_kilomi { get; set; }
        public int par_presion { get; set; }
    }
}