using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace llantasAPP.Models
{

    public class llantasInventario
    {
        public string LLANTA {get;set;}
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
        public int KINSTALA { get; set; }
        public string FECHAI { get; set; }
    }
}