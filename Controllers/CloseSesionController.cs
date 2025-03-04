using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchMaker.Controllers
{
    public class CloseSesionController : Controller
    {
        // GET: CloseSesion
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
                Session["NombreUsuario"] = null;
            }

            // Redirigir al formulario de login
            return RedirectToAction("Login", "Login");
        }
    }
}