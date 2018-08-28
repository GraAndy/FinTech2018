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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.Transactions.Include("Client").Include("Employee").ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Money")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactions);
        }

        [HttpPost]
        public ActionResult CreateAPI(int clientID, int employeeID, int sum)
        {
            Clients client = db.Clients.Find(clientID);
            Employee emp = db.Employes.Find(employeeID);
            if (client != null && emp != null && sum != 0)
            {
                Transactions tr = new Transactions();
                tr.Client = client;
                tr.Employee = emp;
                tr.Date = DateTime.Now;
                tr.Money = sum;
                db.Transactions.Add(tr);
                client.Points = client.Points + (int)(sum * 0.05);
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                ViewData["Message"] = "Success";
            }
            else
            {
                ViewData["Message"] = "Error";
            }
            
            return View();

        }
        public ActionResult CreateAPI()
        {
            return View();
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Money")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
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
