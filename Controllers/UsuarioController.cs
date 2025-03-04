using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Usuario.ToList());
            }
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Usuario.Where(x => x.IdUser == id).FirstOrDefault());
            }
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Usuario.Add(usuario);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                // Obtener el usuario que intenta editar
                var usuario = context.Usuario.Where(x => x.IdUser == id).FirstOrDefault();

                if (usuario == null)
                {
                    return HttpNotFound();
                }

                // Verificar si el usuario autenticado es el mismo que el que está intentando editar
                if (usuario.Usuario1 != nombreUsuario)
                {
                    // Si no es el mismo usuario, ocultar la contraseña en la vista
                    ViewBag.ErrorMessage = "No tienes permiso para editar la contraseña de otro usuario.";
                    // Redirigir o mostrar un mensaje de error si es necesario
                    return View(usuario);
                }

                return View(usuario);
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Usuario.Where(x => x.IdUser == id).FirstOrDefault());
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Usuario usuario = context.Usuario.Where(x => x.IdUser == id).FirstOrDefault();
                    context.Usuario.Remove(usuario);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
