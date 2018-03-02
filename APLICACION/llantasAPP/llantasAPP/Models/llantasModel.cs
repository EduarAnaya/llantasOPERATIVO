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
}