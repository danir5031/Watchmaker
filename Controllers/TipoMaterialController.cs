using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class TipoMaterialController : Controller
    {
        // GET: TipoMaterial
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.TipoArt.ToList());
            }
        }

        // GET: TipoMaterial/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.TipoArt.Where(x => x.IdTipo == id).FirstOrDefault());
            }
        }

        // GET: TipoMaterial/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }

        // POST: TipoMaterial/Create
        [HttpPost]
        public ActionResult Create(TipoArt articulo)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.TipoArt.Add(articulo);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoMaterial/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {

                return View(context.TipoArt.Where(x => x.IdTipo == id).FirstOrDefault());

            }
        }

        // POST: TipoMaterial/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TipoArt articulo)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoMaterial/Delete/5
        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.TipoArt.Where(x => x.IdTipo == id).FirstOrDefault());
            }
        }

        // POST: TipoMaterial/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    TipoArt articulo = context.TipoArt.Where(x => x.IdTipo == id).FirstOrDefault();
                    context.TipoArt.Remove(articulo);
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
