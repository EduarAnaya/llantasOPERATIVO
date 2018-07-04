using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace llantasAPP.Models.bd_con
{
    public class VarGlobals
    {
        public string Usuario { get; set; }
        public int Oficina { get; set; }
        public int Almacen { get; set; }
        public int KmOfic { get; set; }

        public static VarGlobals variablesGlobales { get; set; }
    }
}