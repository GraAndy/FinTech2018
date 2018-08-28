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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static int tempOrgID = 1;

        // GET: Employees
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Index()
        {
            return View(db.Employes.ToList());
        }

        // GET: Employees
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Groups()
        {
            return View();
        }

        // GET: Employees
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Organization()
        {
            List<Employee> empls = db.Employes.Where(e => e.organization.Id == tempOrgID).ToList();
            return View(empls);
        }


        // POST: Employees
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Organization(int EmployesId)
        {
            Employee employee = db.Employes.Find(EmployesId);
            if(employee != null)
            {
                Organization org = db.Organizations.Find(tempOrgID);
                org.Employes.Add(employee);
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(db.Employes.Where(e => e.organization.Id == tempOrgID).ToList());
        }

        // GET: Employees/Details/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employes.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Patronymic")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Wallet = Guid.NewGuid();
                db.Employes.Add(employee);
                db.SaveChanges();

                return View(db.Employes.ToList()[db.Employes.ToList().Count-1]);
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employes.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Patronymic,Wallet")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employes.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [Authorize(Users = "info@fintech.ru")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employes.Find(id);
            db.Employes.Remove(employee);
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
