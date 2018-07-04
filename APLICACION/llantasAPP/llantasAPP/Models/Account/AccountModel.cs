using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using llantasAPP.Models.bd_con;
using llantasAPP.Models.edicionLlantas;

namespace llantasAPP.Models.Account
{
    public class AccountModel
    {
        //private VarGlobals varGlobal = new VarGlobals();

        private List<Account> listAccount = new List<Account>();

        public Account openSesion(AccountViewModel avm, paramDbConexion pDbC)
        {
            procLlantas Proc = new procLlantas(pDbC);
            Account ac = new Account();

            string[] getUSusario = Proc.PDB_CONEXION_LLANTAS_2_0(avm.account.Usuario.ToUpper(), avm.account.Contrasena.ToUpper(), avm.account.Oficina);
            if (getUSusario[0] == "1")//el susuario existe
            {
                #region DEFINICION DE VARIABLES GOBALES
                VarGlobals vg = new VarGlobals();
                vg.Usuario = avm.account.Usuario.ToUpper();
                vg.Oficina = avm.account.Oficina;
                vg.Almacen = int.Parse(getUSusario[1].ToString());
                vg.KmOfic = int.Parse(getUSusario[2].ToString());
                VarGlobals.variablesGlobales = vg;
                #endregion

                ac.Usuario = avm.account.Usuario.ToUpper();
                listAccount.Add(new Account { Usuario = avm.account.Usuario.ToUpper(), Roles = new string[] { "Admin" } });
                return ac;
            }
            else
            {
                listAccount = null;
                return null;
            }
        }


        public Account find(string _Usuario)
        {
            return listAccount.Where(acc => acc.Usuario.Equals(_Usuario)).FirstOrDefault();
        }
        public Account Login(string _Usuario)
        {
            return listAccount.Where(acc => acc.Usuario.Equals(_Usuario)).FirstOrDefault();
        }






        //public VarGlobals Login(paramSession ps)
        //{
        //    try
        //    {
        //        AccountPros Proc = new AccountPros();
        //        string[] getUSusario = Proc.PDB_CONEXION_LLANTAS_2_0(ps);
        //        if (getUSusario[0] == "1")//el susuario existe
        //        {
        //            varGlobal.Usuario = ps.Usuario;
        //            varGlobal.almacen = int.Parse(getUSusario[1].ToString());
        //            return varGlobal;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
    }
}