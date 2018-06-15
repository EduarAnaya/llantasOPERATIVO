using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Linq;
using System.Web;
using System.Data;

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
                throw oraEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
    public partial class llantas_Edit
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


        public DataTable FDB_DATOS_PLACA_2_0(string placa)
        {
            bdConexion bd = new bdConexion();
            DataTable dt = new DataTable();
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.Connection = bd.Conectar();
                comando.CommandText = "FDB_DATOS_PLACA_2_0";
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter refCursor = new OracleParameter();
                refCursor.OracleDbType = OracleDbType.RefCursor;
                refCursor.Direction = ParameterDirection.ReturnValue;
                comando.Parameters.Add(refCursor);

                OracleParameter PLACA = new OracleParameter("PLACA", OracleDbType.Varchar2, 6);
                string _placa = placa.ToUpper();
                PLACA.Value = _placa;
                comando.Parameters.Add(PLACA);

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
        }

    }
    public partial class llantas_Edit
    {
        public string placa { get; set; }
        public int tipoVehiculo { get; set; }
        public int nroEjes { get; set; }
        public int llantasMax { get; set; }
        public int km { get; set; }
        public int llantasCargadas { get; set; }
        public string fechaSistema { get; set; }
        public bool montar { get; set; }
    }
    public class llantas_Monta
    {
        public string par_vehiculo_e { get; set; }
        public string par_llanta_e { get; set; }
        public string par_grupo_e { get; set; }
        public string par_profi_e { get; set; }
        public string par_profc_e { get; set; }
        public string par_profd_e { get; set; }
        public string par_posicion_e { get; set; }
        public int par_kilomi_e { get; set; }
        public DateTime par_fechai_e { get; set; }
        public int par_presion_e { get; set; }
        public int response { get; set; }

    }
    public class llantas_Rota
    {
        public string par_llanta { get; set; }
        public string par_grupo { get; set; }
        public int par_posicion_ini { get; set; }
        public int par_posicion_fin { get; set; }
        public int response { get; set; }

    }

    public class llantas_Desmonta
    {
        public string par_vehiculo_e { get; set; }
        public string par_llanta_e { get; set; }
        public string par_grupo_e { get; set; }
        public int par_observacion_e { get; set; }
        public int par_kilomi_e { get; set; }
        public DateTime par_fechai_e { get; set; }
        public int par_posicion_e { get; set; }
        public int response { get; set; }


        public int desmontarLlantas(string _placa, string _llanta, string _grupo, int _observacion, int _kilometraje, DateTime _fechaDesm)
        {
            bdConexion bd = new bdConexion();
            DataTable dt = new DataTable();
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.Connection = bd.Conectar();
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


                OracleParameter retorna = new OracleParameter("par_retorno_s", OracleDbType.Varchar2);
                retorna.Direction = ParameterDirection.Output;
                comando.Parameters.Add(retorna);

                comando.ExecuteNonQuery();
                var re = comando.Parameters["par_retorno_s"].Value.ToString();
                int nro = 0;
                if (re == "null")
                {
                    nro = 1;
                }
                else
                {
                    nro = 2;
                }
                return nro;
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
    }
}