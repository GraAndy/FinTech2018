using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinTech.Models;

namespace FinTech.Controllers
{
    public class PresentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Presents
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Index()
        {
            return View(db.Presents.ToList());
        }

        // GET: Presents/Details/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Present present = db.Presents.Find(id);
            if (present == null)
            {
                return HttpNotFound();
            }
            return View(present);
        }

        // GET: Presents/Create
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Create([Bind(Include = "Id,Name,Cost")] Present present)
        {
            if (ModelState.IsValid)
            {
                db.Presents.Add(present);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(present);
        }

        // GET: Presents/Edit/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Present present = db.Presents.Find(id);
            if (present == null)
            {
                return HttpNotFound();
            }
            return View(present);
        }

        // POST: Presents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cost")] Present present)
        {
            if (ModelState.IsValid)
            {
                db.Entry(present).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(present);
        }

        // GET: Presents/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Present present = db.Presents.Find(id);
            if (present == null)
            {
                return HttpNotFound();
            }
            return View(present);
        }

        // POST: Presents/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Present present = db.Presents.Find(id);
            db.Presents.Remove(present);
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
