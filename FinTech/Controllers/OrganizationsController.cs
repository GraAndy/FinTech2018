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
    public class OrganizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public static int tempOrgID = 1;

        // GET: Organizations
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Index()
        {
            return View(db.Organizations.ToList());
        }

        [Authorize(Users = "info@fintech.ru")]
        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        [Authorize(Users = "info@fintech.ru")]
        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
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
