using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace llantasAPP.Models.bd_con
{
    public class paramDbConexion
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

        public paramDbConexion(string _Servidor, string _Dbname, string _Usuario, string _Contraseña)
        {
            this.Servidor = _Servidor;
            this.Dbname = _Dbname;
            this.Usuario = _Usuario;
            this.Contrasena = _Contraseña;

        }
        public static paramDbConexion parametrosConexion { get; set; }
    }
}