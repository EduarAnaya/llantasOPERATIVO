using System;
using System.Collections;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Configuration;
using llantasAPP.Models;


namespace llantasAPP
{
    public class bdConexion
    {
        //***********************************************************
        private OracleConnection ora_Connection;
        private string cadena;

        //***********************************************************
        public OracleConnection Ora_Connection
        {
            get { return ora_Connection; }
            set { ora_Connection = value; }
        }

        public string Cadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        paramConn paramConn = new paramConn();

        //contructor
        public bdConexion()
        {
            //this.Conectar();
            string _servidor, _dbname, _usuario, _contrasena;
            _servidor = paramConn.Servidor;
            _dbname = paramConn.Dbname;
            _usuario = paramConn.Usuario;
            _contrasena = paramConn.Contrasena;
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
                return ora_Connection;
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
                if (ora_Connection != null)
                {
                    if (ora_Connection.State != ConnectionState.Closed)
                    {
                        ora_Connection.Close();
                    }
                }
                // Liberamos su memoria.
                ora_Connection.Dispose();
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

        //public DataTable select(string[] _campo, string[] _tabla, string _condicion)
        //{
        //    string query = "SELECT ";
        //    int ultmioC = _campo.Length;

        //    //CONSTRUCCION SELECT//
        //    for (int i = 0; i < _campo.Length; i++)
        //    {
        //        query = query + _campo[i];
        //        if (i < ultmioC - 1)
        //        {
        //            query = query + ",";
        //        }
        //        else
        //        {
        //            query = query + " FROM ";
        //        }
        //    }
        //    int ultmioT = _tabla.Length;

        //    for (int i = 0; i < _tabla.Length; i++)
        //    {
        //        query = query + _tabla[i];
        //        if (i < ultmioT - 1)
        //        {
        //            query = query + ",";
        //        }
        //        else
        //        {
        //            query = query + " ";
        //        }
        //    }
        //    if (_condicion != "")
        //    {
        //        query = query + " WHERE " + _condicion;
        //    }

        //    DataTable dt = new DataTable();
        //    dt.Load(ejecutar_select(query));
        //    return dt;
        //}

        //private OracleDataReader ejecutar_select(string consulta)
        //{
        //    OracleDataReader respuesta;
        //    comando = new OracleCommand(consulta, Ora_Connection);
        //    OracleTransaction transaccion;
        //    transaccion = Ora_Connection.BeginTransaction(IsolationLevel.ReadCommitted);//bloquear los dato tomados para no generar errores
        //    comando.Transaction = transaccion;
        //    try
        //    {
        //        respuesta = comando.ExecuteReader();
        //        transaccion.Commit();
        //        return respuesta;

        //    }
        //    catch (Exception)
        //    {
        //        transaccion.Rollback();
        //        throw;
        //    }
        //}

        //public void rotarLLantaImport(string _llanta, int _kinstala, string _fechai, string _vehiculo, int _posicion)
        //{
        //    string _query = "UPDATE LLANTAS " +
        //        "SET LLANTA =  (SELECT LLANTA FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "GRUPO= ( SELECT GRUPO FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "VALOR=( SELECT VALOR FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "FECHA =( SELECT FECHA FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "PROVEE=( SELECT PROVEE FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "FACTURA=( SELECT FACTURA FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "FICHA=( SELECT FICHA FROM INVENTARIO WHERE LLANTA='" + _llanta + "')," +
        //        "NEUMA       =0," +
        //        "VALORRN     =0," +
        //        "PROTEC      =0," +
        //        "VALORP      =0," +
        //        "KINSTALA    =" + _kinstala + "," +
        //        "FECHAI      ='" + _fechai + "'" +
        //        "WHERE VEHICULO='" + _vehiculo + "'" +
        //        "AND POSICION  =" + _posicion + ";";



        //    comando = new OracleCommand(_query, Ora_Connection);
        //    OracleTransaction transaccion;
        //    transaccion = Ora_Connection.BeginTransaction(IsolationLevel.Serializable);//bloquear los dato tomados para no generar errores
        //    comando.Transaction = transaccion;
        //    try
        //    {
        //        int respuesta = comando.ExecuteNonQuery();
        //        transaccion.Commit();

        //    }
        //    catch (Exception)
        //    {
        //        transaccion.Rollback();
        //        throw;
        //    }

        //}

        //public int rotarLLantaLocal(string _LLANTA, string _GRUPO, int _VALOR, string _FECHA, int _PROVEE, int _FACTURA, int _FICHA, int _KINSTALA, string _FECHAI, string _VEHICULO, int _POSICION)
        //{
        //    int respuesta = 0;
        //    string _query = "INSERT INTO LLANTAS VALUES ('"
        //        + _LLANTA + "','" + _GRUPO + "'," + _VALOR + ","
        //        + "to_date('" + _FECHA + "', 'dd/mm/yyyy'),"
        //        + _PROVEE + "," + _FACTURA + ","
        //        + _FICHA + ",0,0,0,0,'" + _VEHICULO + "' ,"
        //        + _POSICION + " ," + _KINSTALA + ", to_date('" + _FECHAI + "', 'dd/mm/yyyy'))";

        //    comando = new OracleCommand(_query, Ora_Connection);
        //    OracleTransaction transaccion;
        //    transaccion = Ora_Connection.BeginTransaction(IsolationLevel.Serializable);//bloquear los dato tomados para no generar errores
        //    comando.Transaction = transaccion;
        //    try
        //    {
        //        respuesta = comando.ExecuteNonQuery();
        //        transaccion.Commit();
        //        respuesta = 1;
        //        return respuesta;

        //    }
        //    catch (Exception)
        //    {
        //        transaccion.Rollback();
        //        return respuesta;
        //    }
        //}

        //public int eliminarLlanta(string _TABLA, string _CONDICION)
        //{
        //    int respuesta = 0;
        //    //string _query = "DELETE FROM LLANTAS WHERE LLANTA='" + _LLANTA + "' AND VEHICULO='" + _VEHICULO + "'";
        //    string _query = "DELETE FROM " + _TABLA + " " + _CONDICION;

        //    comando = new OracleCommand(_query, Ora_Connection);
        //    OracleTransaction transaccion;
        //    transaccion = Ora_Connection.BeginTransaction(IsolationLevel.Serializable);//bloquear los dato tomados para no generar errores
        //    comando.Transaction = transaccion;
        //    try
        //    {
        //        respuesta = comando.ExecuteNonQuery();
        //        transaccion.Commit();
        //        respuesta = 1;
        //        return respuesta;

        //    }
        //    catch (Exception)
        //    {
        //        transaccion.Rollback();
        //        return respuesta;
        //    }
        //}


        //public string desmontarLlantas
        //    (string par_vehiculo, string par_llanta, string par_grupo, string par_profi, string par_profc, string par_profd, int par_posicion, int par_kilomi, int par_presion)
        //{
        //    string resu = string.Empty;
        //    return resu;
        //}


    }



}
