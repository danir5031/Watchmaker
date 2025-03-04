using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Categoria.ToList());
            }
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                return View(context.Categoria.Where(x => x.idCate == id).FirstOrDefault());
            }
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Categoria.Add(categoria);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {

                return View(context.Categoria.Where(x => x.idCate == id).FirstOrDefault());

            }
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Categoria categoria)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(categoria).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Categoria.Where(x => x.idCate == id).FirstOrDefault());
            }
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Categoria categoria = context.Categoria.Where(x => x.idCate == id).FirstOrDefault();
                    context.Categoria.Remove(categoria);
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
