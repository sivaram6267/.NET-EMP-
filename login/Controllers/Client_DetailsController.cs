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
    public class Client_DetailsController : Controller
    {
        private Emp_Entities db = new Emp_Entities();

        // GET: Client_Details
        public ActionResult Index()
        {
            var client_Details = db.Client_Details.Include(c => c.Employee_details);
            return View(client_Details.ToList());
        }

        // GET: Client_Details/Details/5
        public ActionResult Details(int? id)
        {

            Client_Details client_Details = db.Client_Details.Where(x => x.Emp_id_fk == id).FirstOrDefault();
            if (client_Details == null)
            {
                return HttpNotFound();
            }
            return View(client_Details);
        }

        // GET: Client_Details/Create
        public ActionResult Create()
        {
            ViewBag.Emp_id_fk = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name");
            return View();
        }

        // POST: Client_Details/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client_Details client_Details)
        {
            if (ModelState.IsValid)
            {
                db.Client_Details.Add(client_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Emp_id_fk = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", client_Details.Emp_id_fk);
            return View(client_Details);
        }

        // GET: Client_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_Details client_Details = db.Client_Details.Find(id);
            if (client_Details == null)
            {
                return HttpNotFound();
            }
           ViewBag.Emp_id_fk = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", client_Details.Emp_id_fk);
            return View(client_Details);
        }

        // POST: Client_Details/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client_Details client_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Emp_id_fk = new SelectList(db.Employee_details, "Emp_Id", "Emp_Name", client_Details.Emp_id_fk);
            return View(client_Details);
        }

        // GET: Client_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_Details client_Details = db.Client_Details.Find(id);
            if (client_Details == null)
            {
                return HttpNotFound();
            }
            return View(client_Details);
        }

        // POST: Client_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client_Details client_Details = db.Client_Details.Find(id);
            db.Client_Details.Remove(client_Details);
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
