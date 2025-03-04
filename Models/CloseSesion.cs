using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchMaker.Models
{
    public class CloseSesion : Controller
    {
        // Acción de cerrar sesión
        public ActionResult Logout()
        {
            // Eliminar las variables de sesión
            Session["UsuarioAutenticado"] = null;

            // Eliminar cookies de autenticación si es necesario
            if (Request.Cookies[".AspNet.ApplicationCookie"] != null)
            {
                var cookie = new HttpCookie(".AspNet.ApplicationCookie")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection
                };
                Response.Cookies.Add(cookie);
            }

            // Redirigir al formulario de login
            return RedirectToAction("Login", "Login");
        }
    }
}
 