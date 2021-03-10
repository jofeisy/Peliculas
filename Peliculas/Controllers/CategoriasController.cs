using Peliculas.Data;
using Peliculas.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Peliculas.Controllers
{
    public class CategoriasController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Listar()
        {
            return View(db.Categorias
                .Include(c => c.Peliculas)
                .ToList());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();

                return RedirectToAction("Listar");
            }

            return View(categoria);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Listar");
            }

            return View(categoria);
        }

        public JsonResult Delete(int? id)
        {
            string message = "Registro Eliminado!";

            if (id == null)
            {
                message = "Registro no encontrado";
                return Json(message);
            }

            Categoria categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                message = "Registro no encontrado";
                return Json(message);
            }

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return Json(message);
        }

        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = db.Categorias
                .Include(c => c.Peliculas)
                .FirstOrDefault(m => m.Id == id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
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
