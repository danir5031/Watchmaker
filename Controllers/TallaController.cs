using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class TallaController : Controller
    {
        // GET: Talla
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Tallas.ToList());
            }
        }

        // GET: Talla/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                return View(context.Tallas.Where(x => x.idtalla == id).FirstOrDefault());
            }
        }

        // GET: Talla/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }

        // POST: Talla/Create
        [HttpPost]
        public ActionResult Create(Tallas tallas)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Tallas.Add(tallas);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Talla/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {

                return View(context.Tallas.Where(x => x.idtalla == id).FirstOrDefault());

            }
        }

        // POST: Talla/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tallas tallas)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(tallas).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Talla/Delete/5
        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Tallas.Where(x => x.idtalla == id).FirstOrDefault());
            }
        }

        // POST: Talla/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Tallas tallas = context.Tallas.Where(x => x.idtalla == id).FirstOrDefault();
                    context.Tallas.Remove(tallas);
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
