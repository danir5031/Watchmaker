using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WatchMaker.Models
{
    public class Autenticado : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Verificar si el usuario está autenticado en la sesión
            bool estaAutenticado = httpContext.Session["UsuarioAutenticado"] as bool? ?? false;

            if (estaAutenticado)
            {
                // Si el usuario está autenticado, asignar el nombre del usuario
                string nombreUsuario = httpContext.Session["NombreUsuario"] as string;
                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    httpContext.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(nombreUsuario), null);
                }
            }

            return estaAutenticado;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Si el usuario no está autenticado, redirige a la página de login
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "Login" }
                });
        }
    }
}