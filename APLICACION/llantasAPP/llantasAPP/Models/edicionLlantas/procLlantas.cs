using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Data;
using System.Linq;
using System.Web;

namespace llantasAPP.Models.edicionLlantas
{
    public class procLlantas
    {
        public static bdConexion bd = new bdConexion();
        #region INVENTARIO
        public DataTable select_inventario()
        {
            string consulta = "SELECT * FROM INVENTARIO";//pro de eivn
            bdConexion bd = new bdConexion();
            DataTable dt = new DataTable();
            try
            {
                OracleConnection Ora_Connection = bd.Conectar();
                OracleCommand comando = new OracleCommand(consulta, Ora_Connection);
                if (Ora_Connection != null)
                {
                    dt.Load(comando.ExecuteReader());
                    bd.Desconectar();
                    return dt;
                }
                else
                {
                    return dt;
                }
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
            bdConexion bd = new bdConexion();
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
        public DataTable FDB_DATOS_PLACA_2_0(string placa)
        {
            bdConexion bd = new bdConexion();
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
            bdConexion bd = new bdConexion();
            DataTable dt = new DataTable();
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "FDB_LLANTAS_DISPONIBLES_2_0";
                comando.CommandType = CommandType.StoredProcedure;

                OracleParameter CURSOR = new OracleParameter("l_cursor", OracleDbType.RefCursor);
                CURSOR.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(CURSOR);

                comando.Connection = bd.Conectar();
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
        }
        public int PDB_MONTARLLANTA_2_0(string _placa, string _llanta, string _grupo, int _par_profi_e, int _par_profc_e, int _par_profd_e, int _posicion, int _kilometraje, DateTime _fechaMonta, int _par_presion_e)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_MONTARLLANTA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter PLACA = new OracleParameter("par_vehiculo_e", OracleDbType.Char, 6, ParameterDirection.Input);
                string _Placa = _placa.ToUpper();
                PLACA.Value = _Placa;
                comando.Parameters.Add(PLACA);

                OracleParameter LLANTA = new OracleParameter("par_llanta_e", OracleDbType.Varchar2, 15, ParameterDirection.Input);
                LLANTA.Value = _llanta;
                comando.Parameters.Add(LLANTA);

                OracleParameter GRUPO = new OracleParameter("par_grupo_e", OracleDbType.Char, 3, ParameterDirection.Input);
                GRUPO.Value = _grupo;
                comando.Parameters.Add(GRUPO);

                OracleParameter PROFIZQ = new OracleParameter("par_profi_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFIZQ.Value = _par_profi_e;
                comando.Parameters.Add(PROFIZQ);

                OracleParameter PROFCEN = new OracleParameter("par_profc_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFCEN.Value = _par_profc_e;
                comando.Parameters.Add(PROFCEN);

                OracleParameter PROFDER = new OracleParameter("par_profd_e", OracleDbType.Int32, ParameterDirection.Input);
                PROFDER.Value = _par_profd_e;
                comando.Parameters.Add(PROFDER);

                OracleParameter POSICION = new OracleParameter("par_posicion_e", OracleDbType.Int32, ParameterDirection.Input);
                POSICION.Value = _posicion;
                comando.Parameters.Add(POSICION);

                OracleParameter KILOMETRAJE = new OracleParameter("par_kilomi_e", OracleDbType.Int32, ParameterDirection.Input);
                KILOMETRAJE.Value = _kilometraje;
                comando.Parameters.Add(KILOMETRAJE);

                OracleParameter FECMONTA = new OracleParameter("par_fechai_e", OracleDbType.Date, ParameterDirection.Input);
                FECMONTA.Value = _fechaMonta;
                comando.Parameters.Add(FECMONTA);

                OracleParameter PRESION = new OracleParameter("par_presion_e", OracleDbType.Int32, ParameterDirection.Input);
                PRESION.Value = _par_presion_e;
                comando.Parameters.Add(PRESION);

                OracleParameter retorna = new OracleParameter("par_retorno_s", OracleDbType.Int32);
                retorna.Direction = ParameterDirection.Output;
                comando.Parameters.Add(retorna);
                #endregion

                #region EJECUCION COMANDO + RETURNVALUE
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                int resultadoTransaccion = int.Parse(comando.Parameters["par_retorno_s"].Value.ToString());
                return resultadoTransaccion;
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
        public int PDB_DESMONTARLLANTA_2_0(string _placa, string _llanta, string _grupo, int _observacion, int _kilometraje, DateTime _fechaDesm)
        {
            #region BLOQUE CONTROLADO
            try
            {
                #region DEFINICION DE PARAMETROS DEL COMANDO
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "PDB_DESMONTARLLANTA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter PLACA = new OracleParameter("par_vehiculo_e", OracleDbType.Char, 6);
                PLACA.Direction = ParameterDirection.Input;
                PLACA.Value = _placa;
                comando.Parameters.Add(PLACA);

                OracleParameter LLANTA = new OracleParameter("par_llanta_e", OracleDbType.Varchar2, 15);
                LLANTA.Direction = ParameterDirection.Input;
                LLANTA.Value = _llanta;
                comando.Parameters.Add(LLANTA);

                OracleParameter GRUPO = new OracleParameter("par_grupo_e", OracleDbType.Char, 3, ParameterDirection.Input);
                GRUPO.Value = _grupo;
                comando.Parameters.Add(GRUPO);

                OracleParameter OBSERVACION = new OracleParameter("par_observacion_e", OracleDbType.Int32, ParameterDirection.Input);
                OBSERVACION.Value = _observacion;
                comando.Parameters.Add(OBSERVACION);

                OracleParameter KILOMETRAJE = new OracleParameter("par_kilomi_e", OracleDbType.Int32, ParameterDirection.Input);
                KILOMETRAJE.Value = _kilometraje;
                comando.Parameters.Add(KILOMETRAJE);

                OracleParameter FECDESMONTA = new OracleParameter("par_fechai_e", OracleDbType.Date, ParameterDirection.Input);
                FECDESMONTA.Value = _fechaDesm;
                comando.Parameters.Add(FECDESMONTA);


                OracleParameter retorna = new OracleParameter("par_retorno_s", OracleDbType.Int32);
                retorna.Direction = ParameterDirection.Output;
                comando.Parameters.Add(retorna);
                #endregion

                #region EJECUCION DEL COMANDO + RETURNVALUE
                DataTable dt = new DataTable();
                comando.Connection = bd.Conectar();
                comando.ExecuteNonQuery();
                bd.Desconectar();
                int resultadoTransaccion = int.Parse(comando.Parameters["par_retorno_s"].Value.ToString());
                return resultadoTransaccion;
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
        #endregion
    }
}