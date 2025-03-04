using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchMaker.Models;
using System.Data.Entity;

namespace WatchMaker.Controllers
{
    [Autenticado]
    public class ArticuloController : Controller
    {
        // GET: Articulo
        public ActionResult Index()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                var articulos = context.Articulos.Include(a => a.Imagenes1).ToList(); // Incluir imágenes

                ViewBag.Usuarios = context.Usuario.ToDictionary(u => u.IdUser, u => u.Nombre);
                ViewBag.Tiposart = context.TipoArt.ToDictionary(u => u.IdTipo, u => u.Tipo);
                ViewBag.Catego = context.Categoria.ToDictionary(u => u.idCate, u => u.Categoria1);
                ViewBag.Tallas = context.Tallas.ToDictionary(u => u.idtalla, u => u.Talla);
                ViewBag.Materials = context.Material.ToDictionary(u => u.IdMaterial, u => u.Tipo);

                return View(articulos);
            }
        }

        // GET: Articulo/Details/5
        public ActionResult Details(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                var articulo = context.Articulos
                    .Include(a => a.Imagenes1) // Incluir imágenes relacionadas
                    .FirstOrDefault(x => x.IdArticulo == id);

                if (articulo == null)
                {
                    return HttpNotFound();
                }

                // Buscar el usuario correspondiente al IdUser del artículo
                var usuario = context.Usuario.FirstOrDefault(u => u.IdUser == articulo.IdUser);
                ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Desconocido";

                var tipo = context.TipoArt.FirstOrDefault(t => t.IdTipo == articulo.IdTipo);
                ViewBag.NombreTipo = tipo != null ? tipo.Tipo : "No especificado";

                var categoria = context.Categoria.FirstOrDefault(c => c.idCate == articulo.IdCate);
                ViewBag.NombreCategoria = categoria != null ? categoria.Categoria1 : "No especificado";

                var material = context.Material.FirstOrDefault(m => m.IdMaterial == articulo.IdMaterial);
                ViewBag.NombreMaterial = material != null ? material.Tipo : "No especificado";

                var talla = context.Tallas.FirstOrDefault(t => t.idtalla == articulo.IdTalla);
                ViewBag.NombreTalla = talla != null ? talla.Talla : "No especificado";

                return View(articulo);
            }
        }



        // GET: Articulo/Create
        public ActionResult Create()
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;
            using (DbModels context = new DbModels())
            {
                List<TipoArt> listatipo = context.TipoArt.ToList();
                ViewBag.listatipo = new SelectList(listatipo, "IdTipo", "Tipo");

                List<Categoria> listacate = context.Categoria.ToList();
                ViewBag.listacate = new SelectList(listacate, "idCate", "Categoria1");

                List<Material> listamater = context.Material.ToList();
                ViewBag.listamater = new SelectList(listamater, "IdMaterial", "Tipo");

                List<Tallas> listatalla = context.Tallas.ToList();
                ViewBag.listatalla = new SelectList(listatalla, "idTalla", "Talla");

                return View();
            }


        }

        // POST: Articulo/Create
        // POST: Articulo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Articulos articulo, HttpPostedFileBase ImagenFile)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    string nombreUsuario = HttpContext.User.Identity.Name;

                    // Buscar el ID del usuario autenticado
                    var usuario = context.Usuario.FirstOrDefault(u => u.Nombre == nombreUsuario);

                    if (usuario != null)
                    {
                        articulo.IdUser = usuario.IdUser;
                    }

                    // Agregar el artículo a la base de datos primero para obtener el IdArticulo
                    context.Articulos.Add(articulo);
                    context.SaveChanges(); // Guarda el artículo primero para obtener el Id

                    // Guardar la imagen si se ha subido una
                    if (ImagenFile != null && ImagenFile.ContentLength > 0)
                    {
                        // Crear el objeto Imagen para almacenar la imagen
                        var imagen = new Imagenes();
                        using (var ms = new MemoryStream())
                        {
                            ImagenFile.InputStream.CopyTo(ms);
                            imagen.Imagen = ms.ToArray();
                        }

                        // Asignar el IdArticulo a la imagen
                        imagen.IdArticulo = articulo.IdArticulo; // Asignar el Id del artículo

                        // Agregar la imagen a la base de datos
                        context.Imagenes.Add(imagen);
                        context.SaveChanges();  // Guarda la imagen

                        // Asignar el IdImagen al artículo
                        articulo.IdImagen = imagen.Id; // Asignar el Id de la imagen al artículo
                        context.Entry(articulo).State = System.Data.Entity.EntityState.Modified; // Marcar el artículo como modificado
                        context.SaveChanges(); // Guardar los cambios en el artículo
                    }

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(articulo);
            }
        }





        // GET: Articulo/Edit/5
        public ActionResult Edit(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                // Cargar el artículo y las imágenes asociadas
                Articulos articulos = context.Articulos.Include(a => a.Imagenes1).FirstOrDefault(a => a.IdArticulo == id);

                if (articulos == null)
                {
                    return HttpNotFound();
                }

                List<TipoArt> listatipo = context.TipoArt.ToList();
                ViewBag.listaTipo = new SelectList(listatipo, "IdTipo", "Tipo", articulos.IdTipo);

                List<Categoria> listacate = context.Categoria.ToList();
                ViewBag.listacate = new SelectList(listacate, "idCate", "Categoria1", articulos.IdCate);

                List<Material> listamater = context.Material.ToList();
                ViewBag.listamater = new SelectList(listamater, "IdMaterial", "Tipo", articulos.IdMaterial);

                List<Tallas> listatalla = context.Tallas.ToList();
                ViewBag.listatalla = new SelectList(listatalla, "idTalla", "Talla", articulos.IdTalla);

                return View(articulos);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articulos model, HttpPostedFileBase nuevaImagen)
        {
            if (ModelState.IsValid)
            {
                using (DbModels context = new DbModels())
                {
                    var articulo = context.Articulos.Include(a => a.Imagenes1).FirstOrDefault(a => a.IdArticulo == model.IdArticulo);
                    if (articulo != null)
                    {
                        // Actualizar los datos del artículo
                        articulo.NombreArticulo = model.NombreArticulo;
                        articulo.Cantidad = model.Cantidad;
                        articulo.PrecioVenta = model.PrecioVenta;
                        articulo.IdTipo = model.IdTipo;
                        articulo.IdCate = model.IdCate;
                        articulo.IdMaterial = model.IdMaterial;
                        articulo.IdTalla = model.IdTalla;
                        articulo.Existencia = model.Existencia;

                        // **ACTUALIZAR USUARIO QUE EDITÓ**
                        string usuarioActual = HttpContext.User.Identity.Name;
                        var usuario = context.Usuario.FirstOrDefault(u => u.Nombre == usuarioActual); // O usa el campo correcto
                        if (usuario != null)
                        {
                            articulo.IdUser = usuario.IdUser; // Asegúrate de que `IdUser` es el campo correcto
                        }

                        // Manejar la nueva imagen
                        if (nuevaImagen != null && nuevaImagen.ContentLength > 0)
                        {
                            using (var reader = new BinaryReader(nuevaImagen.InputStream))
                            {
                                byte[] nuevaImagenBytes = reader.ReadBytes(nuevaImagen.ContentLength);

                                var imagenExistente = articulo.Imagenes1.FirstOrDefault();
                                if (imagenExistente != null)
                                {
                                    // Eliminar la imagen anterior
                                    context.Imagenes.Remove(imagenExistente);
                                }

                                // Agregar la nueva imagen
                                var nuevaImagenEntidad = new Imagenes { IdArticulo = articulo.IdArticulo, Imagen = nuevaImagenBytes };
                                context.Imagenes.Add(nuevaImagenEntidad);
                            }
                        }

                        context.SaveChanges(); // Guardar cambios en la base de datos
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }






        public ActionResult Delete(int id)
        {
            string nombreUsuario = HttpContext.User.Identity.Name;
            ViewBag.NombreUsuario = nombreUsuario;

            using (DbModels context = new DbModels())
            {
                var articulo = context.Articulos
                    .Include(a => a.Imagenes1) // Incluir la relación con las imágenes
                    .FirstOrDefault(x => x.IdArticulo == id);

                if (articulo == null)
                {
                    return HttpNotFound();
                }

                // Cargar datos adicionales en los ViewBag
                var usuario = context.Usuario.FirstOrDefault(u => u.IdUser == articulo.IdUser);
                ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Desconocido";

                var tipo = context.TipoArt.FirstOrDefault(t => t.IdTipo == articulo.IdTipo);
                ViewBag.NombreTipo = tipo != null ? tipo.Tipo : "No especificado";

                var categoria = context.Categoria.FirstOrDefault(c => c.idCate == articulo.IdCate);
                ViewBag.NombreCategoria = categoria != null ? categoria.Categoria1 : "No especificado";

                var material = context.Material.FirstOrDefault(m => m.IdMaterial == articulo.IdMaterial);
                ViewBag.NombreMaterial = material != null ? material.Tipo : "No especificado";

                var talla = context.Tallas.FirstOrDefault(t => t.idtalla == articulo.IdTalla);
                ViewBag.NombreTalla = talla != null ? talla.Talla : "No especificado";

                return View(articulo);
            }
        }

        // POST: Articulo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    var articulo = context.Articulos
                        .Include(a => a.Imagenes1) // Incluir las imágenes relacionadas
                        .FirstOrDefault(x => x.IdArticulo == id);

                    if (articulo == null)
                    {
                        return HttpNotFound();
                    }

                    // Eliminar imágenes asociadas si existen
                    if (articulo.Imagenes1 != null && articulo.Imagenes1.Any())
                    {
                        context.Imagenes.RemoveRange(articulo.Imagenes1);
                    }

                    // Eliminar el artículo
                    context.Articulos.Remove(articulo);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // Puedes registrar el error para depuración
                ModelState.AddModelError("", "No se pudo eliminar el artículo. Puede que tenga registros asociados.");
                return View();
            }
        }


        public ActionResult ObtenerImagen(int id)
        {
            using (DbModels context = new DbModels())
            {
                var imagen = context.Imagenes.FirstOrDefault(i => i.IdArticulo == id);
                if (imagen != null && imagen.Imagen != null)
                {
                    return File(imagen.Imagen, "image/jpeg"); // Ajusta el tipo MIME si es necesario
                }
            }

            // Imagen por defecto si no hay ninguna
            return File("~/Content/imagenes/default.png", "image/png"); // Asegúrate de tener esta imagen en tu carpeta
        }




    }
}
