using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using login.Models;


namespace login.Controllers
{
    [Authorize]
    public class Bill_Controller : Controller
    {
        private Emp_Entities db = new Emp_Entities();

        // GET: Bill_
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.Employee_details);
            return View(bills.ToList());
        }

        // GET: Bill_/Details/5
        public ActionResult Details(int? id)
        {
            Bill bl = new Bill();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }

            return View(bill);
        }
        

        // GET: Bill_/Create
        public ActionResult Create()
        {
            ViewBag.Emp_Id_FK = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name");
            return View();
        }

        // POST: Bill_/Create 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Emp_Id_FK = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", bill.Emp_Id_FK);
            return View(bill);
        }

        // GET: Bill_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Emp_Id_FK = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", bill.Emp_Id_FK);
            return View(bill);
        }

        // POST: Bill_/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           ViewBag.Emp_Id_FK = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", bill.Emp_Id_FK);
            return View(bill);
        }

        // GET: Bill_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bill_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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











    