using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Material.ToList());
            }
        }

        // GET: Material/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                return View(context.Material.Where(x => x.IdMaterial == id).FirstOrDefault());
            }
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create(Material material)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Material.Add(material);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {

                return View(context.Material.Where(x => x.IdMaterial == id).FirstOrDefault());

            }
        }

        // POST: Material/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Material material)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(material).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Delete/5
        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                return View(context.Material.Where(x => x.IdMaterial == id).FirstOrDefault());
            }
        }

        // POST: Material/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Material material = context.Material.Where(x => x.IdMaterial == id).FirstOrDefault();
                    context.Material.Remove(material);
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
