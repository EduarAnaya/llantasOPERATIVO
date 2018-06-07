using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llantasAPP
{
    class paramConn
    {
        private string servidor;
        private string dbname;
        private string usuario;
        private string contrasena;
        
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }
        public string Servidor
        {
            get { return servidor; }
            set { servidor = value; }
        }
        public string Dbname
        {
            get { return dbname; }
            set { dbname = value; }
        }

        public paramConn() {
            this.Servidor = "192.168.30.11";
            this.Dbname = "MILDESA";
            this.Usuario = "TRANSER";
            this.Contrasena = "DBACONNECT";
        }

    }
}
