using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AZsmart;

namespace AZsmart.Controllers
{
    public class BibliotecaController : Controller
    {
        private AZsmartEntities db = new AZsmartEntities();

        // GET: Biblioteca
        public ActionResult Index()
        {
            var biblioteca = db.Biblioteca.Include(b => b.Autores);
            return View(biblioteca.ToList());
        }

        // GET: Biblioteca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Biblioteca.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // GET: Biblioteca/Create
        public ActionResult Create()
        {
            ViewBag.autor_id = new SelectList(db.Autores, "id", "nombre");
            return View();
        }

        // POST: Biblioteca/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "id,titulo,autor_id")] Biblioteca biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Biblioteca.Add(biblioteca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.autor_id = new SelectList(db.Autores, "id", "nombre", biblioteca.autor_id);
            return View(biblioteca);
        }

        // GET: Biblioteca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Biblioteca.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            ViewBag.autor_id = new SelectList(db.Autores, "id", "nombre", biblioteca.autor_id);
            return View(biblioteca);
        }

        // POST: Biblioteca/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,autor_id")] Biblioteca biblioteca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biblioteca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.autor_id = new SelectList(db.Autores, "id", "nombre", biblioteca.autor_id);
            return View(biblioteca);
        }

        // GET: Biblioteca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biblioteca biblioteca = db.Biblioteca.Find(id);
            if (biblioteca == null)
            {
                return HttpNotFound();
            }
            return View(biblioteca);
        }

        // POST: Biblioteca/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biblioteca biblioteca = db.Biblioteca.Find(id);
            db.Biblioteca.Remove(biblioteca);
            db.SaveChanges();
            return RedirectToAction("Index");
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
