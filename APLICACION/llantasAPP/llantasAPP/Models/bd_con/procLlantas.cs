using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using llantasAPP.Models.bd_con;

namespace llantasAPP.Models.edicionLlantas
{
    public class procLlantas
    {
        private bdConexionSession bd { get; set; }

        public procLlantas(paramDbConexion ps)
        {
            bd = new bdConexionSession(ps);
        }

        //public static bdConexion bd = new bdConexion();

        #region LOGIN
        public string[] PDB_CONEXION_LLANTAS_2_0(string Usuario, string Contrasena, int Oficina)
        {
            string[] ass;

            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DEL COMANDO Y SUS  PARAMETROS
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_CONEXION_LLANTAS_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                OracleParameter USUARIO = new OracleParameter("par_usuario_e", OracleDbType.Varchar2, 200, "", ParameterDirection.Input);
                USUARIO.Value = Usuario;
                comando.Parameters.Add(USUARIO);

                OracleParameter CONTRASENA = new OracleParameter("par_password_e", OracleDbType.Varchar2, 200, "", ParameterDirection.Input);
                CONTRASENA.Value = Contrasena;
                comando.Parameters.Add(CONTRASENA);

                OracleParameter OFICINA = new OracleParameter("par_oficina_e", OracleDbType.Int32, ParameterDirection.Input);
                OFICINA.Value = Oficina;
                comando.Parameters.Add(OFICINA);

                OracleParameter RETORNO = new OracleParameter("par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(RETORNO);

                #region EJECUCION COMANDO + RETURN VALUE
                try
                {
                    comando.Connection = bd.Conectar();
                    comando.ExecuteNonQuery();
                    bd.Desconectar();
                    string respuestaTransaccion = RETORNO.Value.ToString();
                    return renderErrorLogin(respuestaTransaccion);
                }
                catch (OracleException oraExc)
                {
                    if (oraExc.Number == 1017)
                    {
                        bd.Desconectar();
                        return ass = new string[] { "0", "0", "0" };
                    }
                    throw oraExc;
                }
                #endregion

                #endregion
            }
            catch (OracleException oraExc)
            {
                bd.Desconectar();
                throw oraExc;
            }
            catch (Exception Exc)
            {
                bd.Desconectar();
                throw Exc;
            }
            #endregion
        }

        #endregion

        #region RENDER ERROR-Login
        public string[] renderErrorLogin(string templateError)
        {
            var itemsTemplateError = templateError.Split(new char[] { ';' });
            string transacDb = itemsTemplateError[0];//1:transacCorecta;0:error
            string respuesraDb = itemsTemplateError[1];//almacen
            string kmMaximo = itemsTemplateError[2];//Km maximo
            string[] error = new string[] { transacDb, respuesraDb, kmMaximo };
            return error;
        }
        #endregion

        #region INVENTARIO
        public DataTable select_inventario()
        {
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "FDB_LLANTAS_DISPONIBLES_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                OracleParameter CURSOR = new OracleParameter("l_cursor", OracleDbType.RefCursor);
                CURSOR.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(CURSOR);

                comando.Connection = bd.Conectar();
                DataTable dt = new DataTable();
                dt.Load(comando.ExecuteReader());
                bd.Desconectar();
                return dt;
            }
            catch (OracleException oraExc)
            {

                throw oraExc;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }
        #endregion

        #region EDICIONLLANTAS
        public DataTable valVehiculo(string placa, int tVehiculo)
        {
            string consulta = string.Empty;
            if (tVehiculo == 1)//cabezote
            {
                consulta = "SELECT VEHI_PLACA_CH FROM VEHICULOS WHERE VEHI_PLACA_CH='" + placa + "'";
            }
            else if (tVehiculo == 2)
            {
                consulta = "SELECT TRAI_PLACA_CH FROM TRAILERS WHERE TRAI_PLACA_CH='" + placa + "'";
            }
            DataTable dt = new DataTable();
            try
            {
                OracleConnection Ora_Connection = bd.Conectar();
                OracleCommand comando = new OracleCommand(consulta, Ora_Connection);

                dt.Load(comando.ExecuteReader());
                bd.Desconectar();
                return dt;
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }
        }//pendiente para generar en paquete

        #region TRANSACCIONES DE ORDENES DE TRABAJO

        #region VALIDACION
        public int PDB_MIL_VALIDA_OT_2_0(string _tipo, string _elemento, string _serial)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE LOS PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_MIL_VALIDA_OT_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                //TIPO:C= Cabezote, T= Trailer, I= Item
                OracleParameter TIPO = new OracleParameter("par_tipo_e", OracleDbType.Char, 1, "", ParameterDirection.Input);
                TIPO.Value = _tipo;
                comando.Parameters.Add(TIPO);

                //placa cabezote ó placa tráiler ó código del ítem
                OracleParameter ELEMENTO = new OracleParameter("par_elemento_e", OracleDbType.Char, 20, "", ParameterDirection.Input);
                ELEMENTO.Value = _elemento;
                comando.Parameters.Add(ELEMENTO);

                //Código de la llanta
                OracleParameter SERIAL = new OracleParameter("par_serial_e", OracleDbType.Varchar2, 200, "", ParameterDirection.Input);
                SERIAL.Value = _serial;
                comando.Parameters.Add(SERIAL);

                OracleParameter RETORNO = new OracleParameter("par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(RETORNO);

                #endregion

                #region EJECUCION DEL COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                int resultadoTransaccion = int.Parse(RETORNO.Value.ToString());
                return resultadoTransaccion;
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            #endregion
        }
        #endregion

        #region GET INFORMACION
        public DataTable FDB_MIL_DATOS_OTPL_2_0(string par_tipo_e, string par_placa_e)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "FDB_MIL_DATOS_OTPL_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter refCursor = new OracleParameter();
                refCursor.OracleDbType = OracleDbType.RefCursor;
                refCursor.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(refCursor);

                OracleParameter TIPO = new OracleParameter("par_tipo_e", OracleDbType.Char, 200, "", ParameterDirection.Input);
                TIPO.Value = par_tipo_e;
                comando.Parameters.Add(TIPO);


                OracleParameter PLACA = new OracleParameter("par_placa_e", OracleDbType.Char, 6, "", ParameterDirection.Input);
                PLACA.Value = par_placa_e;
                comando.Parameters.Add(PLACA);


                comando.Connection = bd.Conectar();
                OracleDataAdapter da = new OracleDataAdapter(comando);
                da.Fill(dt);
                return dt;
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }

        }
        #endregion

        #endregion

        public DataTable FDB_DATOS_PLACA_2_0(string placa)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "FDB_DATOS_PLACA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter refCursor = new OracleParameter();
                refCursor.OracleDbType = OracleDbType.RefCursor;
                refCursor.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(refCursor);

                OracleParameter PLACA = new OracleParameter("PLACA", OracleDbType.Varchar2, 6);
                PLACA.Direction = ParameterDirection.Input;
                string _placa = placa.ToUpper();
                PLACA.Value = _placa;
                comando.Parameters.Add(PLACA);

                comando.Connection = bd.Conectar();
                OracleDataAdapter da = new OracleDataAdapter(comando);
                da.Fill(dt);
                return dt;
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }
        }

        public DataTable llantasDisponibles()
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "FDB_LLANTAS_DISPONIBLES_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                OracleParameter CURSOR = new OracleParameter("l_cursor", OracleDbType.RefCursor);
                CURSOR.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(CURSOR);
                #endregion

                #region EJECUCION DEL COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                DataTable dt = new DataTable();
                dt.Load(comando.ExecuteReader());
                bd.Desconectar();
                return dt;
                #endregion
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }
            #endregion
        }

        public string[] PDB_MONTARLLANTA_2_0(string _placa, string _llanta, string _grupo, decimal _par_profi_e, decimal _par_profc_e, decimal _par_profd_e, int _posicion, int _kilometraje, DateTime _fechaMonta, decimal _par_presion_e, int _par_sentido)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_MONTARLLANTA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter PLACA = new OracleParameter(":par_vehiculo_e", OracleDbType.Char, 10, "", ParameterDirection.Input);
                string _Placa = _placa.ToUpper();
                PLACA.Value = _Placa;
                comando.Parameters.Add(PLACA);

                OracleParameter LLANTA = new OracleParameter(":par_llanta_e", OracleDbType.Varchar2, 10, "", ParameterDirection.Input);
                LLANTA.Value = _llanta;
                comando.Parameters.Add(LLANTA);

                OracleParameter GRUPO = new OracleParameter(":par_grupo_e", OracleDbType.Char, 3, "", ParameterDirection.Input);
                GRUPO.Value = _grupo;
                comando.Parameters.Add(GRUPO);

                OracleParameter PROFIZQ = new OracleParameter(":par_profi_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFIZQ.Value = _par_profi_e;
                comando.Parameters.Add(PROFIZQ);

                OracleParameter PROFCEN = new OracleParameter(":par_profc_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFCEN.Value = _par_profc_e;
                comando.Parameters.Add(PROFCEN);

                OracleParameter PROFDER = new OracleParameter(":par_profd_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFDER.Value = _par_profd_e;
                comando.Parameters.Add(PROFDER);

                OracleParameter POSICION = new OracleParameter(":par_posicion_e", OracleDbType.Int32, ParameterDirection.Input);
                POSICION.Value = _posicion;
                comando.Parameters.Add(POSICION);

                OracleParameter KILOMETRAJE = new OracleParameter(":par_kilomi_e", OracleDbType.Int32, ParameterDirection.Input);
                KILOMETRAJE.Value = _kilometraje;
                comando.Parameters.Add(KILOMETRAJE);

                OracleParameter FECMONTA = new OracleParameter(":par_fechai_e", OracleDbType.Date, 50, "", ParameterDirection.Input);
                FECMONTA.Value = _fechaMonta;
                comando.Parameters.Add(FECMONTA);

                OracleParameter PRESION = new OracleParameter(":par_presion_e", OracleDbType.Double, ParameterDirection.Input);
                PRESION.Value = _par_presion_e;
                comando.Parameters.Add(PRESION);

                OracleParameter SENTIDO = new OracleParameter(":par_sentido", OracleDbType.Int16, ParameterDirection.Input);
                SENTIDO.Value = _par_sentido;
                comando.Parameters.Add(SENTIDO);

                OracleParameter retorna = new OracleParameter(":par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(retorna);
                #endregion

                #region EJECUCION COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                string resultadoTransaccion = retorna.Value.ToString();
                return renderError(resultadoTransaccion);
                #endregion
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }
            #endregion
        }

        public string[] PDB_DESMONTARLLANTA_2_0(string _placa, string _llanta, string _grupo, int _observacion, int _kilometraje, DateTime _fechaDesm)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_DESMONTARLLANTA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter PLACA = new OracleParameter(":par_vehiculo_e", OracleDbType.Char, 10, "", ParameterDirection.Input);
                PLACA.Value = _placa;
                comando.Parameters.Add(PLACA);

                OracleParameter LLANTA = new OracleParameter(":par_llanta_e", OracleDbType.Varchar2, 10, "", ParameterDirection.Input);
                LLANTA.Value = _llanta;
                comando.Parameters.Add(LLANTA);

                OracleParameter GRUPO = new OracleParameter(":par_grupo_e", OracleDbType.Char, 3, "", ParameterDirection.Input);
                GRUPO.Value = _grupo;
                comando.Parameters.Add(GRUPO);

                OracleParameter OBSERVACION = new OracleParameter(":par_observacion_e", OracleDbType.Int32, ParameterDirection.Input);
                OBSERVACION.Value = _observacion;
                comando.Parameters.Add(OBSERVACION);

                OracleParameter KILOMETRAJE = new OracleParameter(":par_kilomi_e", OracleDbType.Int32, ParameterDirection.Input);
                KILOMETRAJE.Value = _kilometraje;
                comando.Parameters.Add(KILOMETRAJE);

                OracleParameter FECMONTA = new OracleParameter(":par_fechai_e", OracleDbType.Date, 50, "", ParameterDirection.Input);
                FECMONTA.Value = _fechaDesm;
                comando.Parameters.Add(FECMONTA);

                OracleParameter retorna = new OracleParameter(":par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(retorna);
                #endregion

                #region EJECUCION DEL COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                string resultadoTransaccion = retorna.Value.ToString();
                return renderError(resultadoTransaccion);
                #endregion
            }
            catch (OracleException oraEx)
            {
                bd.Desconectar();
                throw oraEx;
            }
            catch (Exception Ex)
            {
                bd.Desconectar();
                throw Ex;
            }
            #endregion
        }

        public string[] PDB_ROTARLLANTA(string _llanta, int _posicion, int _sentido, string _placa)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_CAMBIARPOS_LLANTA_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                OracleParameter LLANTA = new OracleParameter("par_llanta_e", OracleDbType.Varchar2, 10, "", ParameterDirection.Input);
                LLANTA.Value = _llanta;
                comando.Parameters.Add(LLANTA);

                OracleParameter POSICION = new OracleParameter("par_posicion_e", OracleDbType.Varchar2, 10, "", ParameterDirection.Input);
                POSICION.Value = _posicion;
                comando.Parameters.Add(POSICION);

                OracleParameter SENTIDO = new OracleParameter("par_sentido_e", OracleDbType.Char, 6, "", ParameterDirection.Input);
                SENTIDO.Value = _sentido;
                comando.Parameters.Add(SENTIDO);

                OracleParameter PLACA = new OracleParameter("par_vehiculo_e", OracleDbType.Char, 6, "", ParameterDirection.Input);
                PLACA.Value = _placa;
                comando.Parameters.Add(PLACA);

                OracleParameter RETORNO = new OracleParameter("par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(RETORNO);
                #endregion

                #region EJECUCION COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                string resultadoTransaccion = RETORNO.Value.ToString();
                return renderError(resultadoTransaccion);
                #endregion

            }
            catch (OracleException oraExc)
            {
                throw oraExc;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            #endregion
        }

        public string[] PDB_PROFUNDIDAD_2_0(string par_llanta_e, string par_grupo_e, decimal par_profi_e, decimal par_profc_e, decimal par_profd_e, int par_kilom_e, DateTime par_fecham_e, decimal par_presion_e)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_PROFUNDIDAD_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter LLANTA = new OracleParameter("par_llanta_e", OracleDbType.Varchar2, 20, "", ParameterDirection.Input);
                LLANTA.Value = par_llanta_e;
                comando.Parameters.Add(LLANTA);

                OracleParameter GRUPO = new OracleParameter("par_grupo_e", OracleDbType.Char, 3, "", ParameterDirection.Input);
                GRUPO.Value = par_grupo_e;
                comando.Parameters.Add(GRUPO);

                OracleParameter PROFIZQ = new OracleParameter("par_profi_e", OracleDbType.Double, 3, ParameterDirection.Input);
                PROFIZQ.Value = par_profi_e;
                comando.Parameters.Add(PROFIZQ);

                OracleParameter PROFCEN = new OracleParameter("par_profc_e", OracleDbType.Double, 3, ParameterDirection.Input);
                PROFCEN.Value = par_profc_e;
                comando.Parameters.Add(PROFCEN);

                OracleParameter PROFDER = new OracleParameter("par_profd_e", OracleDbType.Double, 3, ParameterDirection.Input);
                PROFDER.Value = par_profd_e;
                comando.Parameters.Add(PROFDER);

                OracleParameter KILOMETRAJE = new OracleParameter("par_kilom_e", OracleDbType.Int32, ParameterDirection.Input);
                KILOMETRAJE.Value = par_kilom_e;
                comando.Parameters.Add(KILOMETRAJE);

                OracleParameter FECMONTA = new OracleParameter("par_fecham_e", OracleDbType.Date, 50, "", ParameterDirection.Input);
                FECMONTA.Value = par_fecham_e;
                comando.Parameters.Add(FECMONTA);

                OracleParameter PRESION = new OracleParameter("par_presion_e", OracleDbType.Double, 3, ParameterDirection.Input);
                PRESION.Value = par_presion_e;
                comando.Parameters.Add(PRESION);

                OracleParameter retorna = new OracleParameter("par_retorno_s", OracleDbType.Varchar2, 200, "", ParameterDirection.Output);
                comando.Parameters.Add(retorna);
                #endregion

                #region EJECUCION COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                string resultadoTransaccion = retorna.Value.ToString();
                return renderError(resultadoTransaccion);
                #endregion
            }
            catch (OracleException oraExc)
            {
                throw oraExc;
            }
            catch (Exception Exc)
            {

                throw Exc;
            }

            #endregion
        }

        #endregion

        #region RENDER ERROR
        public string[] renderError(string templateError)
        {
            var itemsTemplateError = templateError.Split(new char[] { ';' });
            string transacDb = itemsTemplateError[0];
            string respuesraDb = itemsTemplateError[1];
            string traduccionDB = string.Empty;

            switch (respuesraDb)
            {
                case "1":
                    traduccionDB = "Transacción exitosa.";
                    break;
                default:
                    traduccionDB = respuesraDb;
                    break;
            }

            string[] error = new string[] { transacDb, traduccionDB };
            return error;
        }
        #endregion

    }
}