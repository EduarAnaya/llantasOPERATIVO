using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using llantasAPP.Models.Account;
using llantasAPP.Models.bd_con;
using System.Web.Security;
using System.Transactions;
using WebMatrix.WebData;
using llantasAPP.seguridad;

namespace llantasAPP.Controllers.Account
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            try
            {
                string Server = string.Empty;
                string DbName = string.Empty;
                int Oficina = 0;

                if (avm.account.Oficina == 17)//operavito
                {
                    Oficina = avm.account.Oficina;
                    Server = "192.168.30.11";
                    DbName = "MILDESA";
                }
                if (avm.account.Oficina == 11)//duitama
                {
                    Oficina = avm.account.Oficina;
                    Server = "192.168.30.11";
                    DbName = "MILDESA";

                }
                paramDbConexion dpbc = new paramDbConexion(Server, DbName, avm.account.Usuario.ToUpper(), avm.account.Contrasena.ToUpper());
                paramDbConexion.parametrosConexion = dpbc;
                AccountModel am = new AccountModel();
                if (string.IsNullOrEmpty(avm.account.Usuario) || string.IsNullOrEmpty(avm.account.Contrasena) || am.openSesion(avm, paramDbConexion.parametrosConexion) == null)
                {
                    ModelState.AddModelError("ErrorLogin", "Usuario o contraseña son incorrectos.");
                    return View();
                }
                SessionPersister.Account = am.Login(avm.account.Usuario.ToUpper());
                SessionPersister.Username = avm.account.Usuario.ToUpper();
                return RedirectToAction("Index", "Home");

            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("ErrorDesc", Ex.StackTrace);
                return View("_Error");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            SessionPersister.Username = string.Empty;
            return RedirectToAction("Login", "Account");
        }
    }
}
