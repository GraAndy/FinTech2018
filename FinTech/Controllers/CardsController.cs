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
    public class CardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public static int tempOrgID = 1;

        // GET: Cards
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Index(string CardCode, string CardPassword)
        {
            if(CardCode != null)
            {
                Card card = db.Cards.Where(c => c.code == CardCode).FirstOrDefault();
                if (card != null)
                {
                    card.organization = db.Organizations.Find(tempOrgID);
                    if (card.password == CardPassword)
                    {
                        db.Entry(card).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewData["Message"] = "Неверный пароль карты";
                    }
                }
                else
                {
                    ViewData["Message"] = "Карты с таким кодом не найдено";
                }
            }
            else
            {
                ViewData["Message"] = "";
            }
            List<Card> cards = db.Cards.Where(c => c.organization.Id == tempOrgID).ToList();
            return View(cards);
        }

        // GET: Employees
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Employees(int EmployeesID)
        {
            List<Card> cards = db.Cards.Where(c => c.organization.Id == tempOrgID).ToList();
            ViewData["Employee"] = db.Employes.Find(EmployeesID);
            ViewData["Cards"] = cards;
            return View();
        }

        // POST: Employees
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        public ActionResult Employees(int EmployeesID, int[] Cards)
        {
            List<Card> CardsList = db.Cards.ToList();
            Employee emp = db.Employes.Find(EmployeesID);
            foreach(var item in CardsList)
            {
                if(Cards.Contains(item.Id))
                {
                    item.employee = emp;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    if(item.employee != null)
                    {
                        if(item.employee.Id == EmployeesID)
                        {
                            item.employee = null;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
            List<Card> cards = db.Cards.Where(c => c.organization.Id == tempOrgID).ToList();
            ViewData["Employee"] = db.Employes.Find(EmployeesID).Id;
            return RedirectToAction("Organization", "Employees");
        }


        // GET: Cards/Details/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Create([Bind(Include = "Id,code,password")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card);
        }

        // GET: Cards/Edit/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,code,password")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Cards.Find(id);
            db.Cards.Remove(card);
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
