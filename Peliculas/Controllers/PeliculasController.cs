using Peliculas.Data;
using Peliculas.Helpers;
using Peliculas.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Peliculas.Controllers
{
    public class PeliculasController : Controller
    {
        private DataContext db = new DataContext();


        public ActionResult Listar()
        {
            List<Pelicula> peliculas = db.Peliculas.ToList();
            List<Categoria> categorias = db.Categorias.ToList();

            var query = from Peliculas in peliculas
                        join Categorias in categorias on Peliculas.CategoriaId equals Categorias.Id
                        select new PeliculaViewModel()
                        {
                            Id = Peliculas.Id,
                            Titulo = Peliculas.Titulo,
                            Director = Peliculas.Director,
                            FechaPublicacion = Peliculas.FechaPublicacion.ToString("dd/MM/yyyy"),
                            Categoria = Categorias.Descripcion
                        };

            return View(query);
        }

        public ActionResult Crear()
        {
            ViewBag.CategoriaId = new SelectList(DropDownListHelper.ObtenerCategorias(), "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Peliculas.Add(pelicula);
                db.SaveChanges();

                return RedirectToAction("Listar");
            }

            ViewBag.CategoriaId = new SelectList(DropDownListHelper.ObtenerCategorias(), "Id", "Descripcion", pelicula.CategoriaId);

            return View(pelicula);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriaId = new SelectList(DropDownListHelper.ObtenerCategorias(), "Id", "Descripcion", pelicula.CategoriaId);
            return View(pelicula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Listar");
            }

            ViewBag.CategoriaId = new SelectList(DropDownListHelper.ObtenerCategorias(), "Id", "Descripcion", pelicula.CategoriaId);
            return View(pelicula);
        }

        public JsonResult Delete(int? id)
        {
            string message = "Registro Eliminado!";

            if (id == null)
            {
                message = "Registro no encontrado";
                return Json(message);
            }

            Pelicula pelicula = db.Peliculas.Find(id);

            if (pelicula == null)
            {
                message = "Registro no encontrado";
                return Json(message);
            }

            db.Peliculas.Remove(pelicula);
            db.SaveChanges();

            return Json(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}