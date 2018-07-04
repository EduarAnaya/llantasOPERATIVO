using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace llantasAPP.Models.bd_con
{
    public class bdConexionSession
    {
        private OracleConnection Ora_Connection { get; set; }
        private string Cadena { get; set; }

        public bdConexionSession(paramDbConexion _paramDbSession)
        {
            string _servidor, _dbname, _usuario, _contrasena;
            _servidor = _paramDbSession.Servidor;
            _dbname = _paramDbSession.Dbname;
            _usuario = _paramDbSession.Usuario;
            _contrasena = _paramDbSession.Contrasena;
            Cadena = @"User Id=" + _usuario + ";Password="
                + _contrasena + ";Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST="
                + _servidor + ")(PORT=1521))(CONNECT_DATA=(SID=" + _dbname + ")));";
        }

        public OracleConnection Conectar()
        {
            try
            {
                Ora_Connection = new OracleConnection();
                if (Ora_Connection != null)
                {
                    // Fijamos la cadena de conexión de la base de datos.
                    Ora_Connection.ConnectionString = Cadena;
                    Ora_Connection.Open();
                }
                return Ora_Connection;
            }
            catch (OracleException oraEx)
            {
                throw oraEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }
        public bool Desconectar()
        {
            try
            {
                // Cerramos la conexion
                if (Ora_Connection != null)
                {
                    if (Ora_Connection.State != ConnectionState.Closed)
                    {
                        Ora_Connection.Close();
                    }
                }
                // Liberamos su memoria.
                Ora_Connection.Dispose();
                return false;
            }
            catch (OracleException oraEx)
            {
                throw oraEx;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}