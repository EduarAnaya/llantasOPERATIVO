using llantasAPP.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace llantasAPP.seguridad
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filtercontex)
        {
            if (string.IsNullOrEmpty(SessionPersister.Username))
            {
                filtercontex.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else
            {
                AccountModel am = new AccountModel();
                Account a = SessionPersister.Account;
                CustomPrincipal mp = new CustomPrincipal(a);
                if (!mp.IsInRole(Roles))
                {
                    filtercontex.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccesoDenegado", action = "Index" }));
                }
            }
        }
    }
}