using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User oUsuario)
        {
            using (SqlConnection cn = Conexion.Conectar()) // Usamos la conexión en un bloque using
            {
                cn.Open(); // Abrimos la conexión

                using (SqlCommand vali = new SqlCommand("ValidarUsuario", cn))
                {
                    vali.CommandType = CommandType.StoredProcedure;
                    vali.Parameters.AddWithValue("@user", oUsuario.Usuario);
                    vali.Parameters.AddWithValue("@pass", oUsuario.Pass);

                    SqlDataReader reader = vali.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            oUsuario.Id_user = Convert.ToInt32(reader["IdUser"]);
                            string rol = reader["Rol"].ToString();
                            string nombreUsuario = reader["Nombre"].ToString(); // Supón que tienes el campo "Nombre" en tu base de datos

                            // Guardamos el nombre del usuario en la sesión
                            Session["UsuarioAutenticado"] = true;
                            Session["NombreUsuario"] = nombreUsuario; // Guardamos el nombre

                            // Dependiendo del rol, redirigimos
                            if (rol == "admin")
                            {
                                return RedirectToAction("Index", "Home"); // Redirige al index si es admin
                            }
                            else if (rol == "usuario")
                            {
                                return RedirectToAction("About", "Home"); // Redirige a about si es usuario
                            }
                        }
                    }
                    else
                    {
                        // Si no encuentra el usuario, mostramos el mensaje de error
                        ViewData["Mensaje"] = "Correo o contraseña incorrectos";
                        return View();
                    }
                    ViewData["Mensaje"] = "Correo o contraseña incorrectos";
                    return View();
                }
            }
        }
    }
}