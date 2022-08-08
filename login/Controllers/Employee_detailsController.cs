using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using login.Models;
using login.ViewModel;

namespace login.Controllers
{
    [Authorize]
    public class Employee_detailsController : Controller
    {
        public Emp_Entities db = new Emp_Entities();
       
        // GET: Employee_details
        public ActionResult Index()
        {
            GetAllDetails details = new GetAllDetails();

            details.employe = db.Employee_details.ToList();
            details.clientDetails = db.Client_Details.ToList();
            details.billdetails = db.Bills.ToList();

            //return View(db.Employee_details.ToList());
            return View(details);

        }

        // GET: Employee_details/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_details employee_details = db.Employee_details.Where(x => x.Emp_Id == id).FirstOrDefault();
            Bill _empbills = employee_details.Bills.FirstOrDefault();
            Client_Details c = employee_details.Client_Details.Where(x => x.Emp_id_fk == id).FirstOrDefault();

            DateTime Jd = DateTime.Parse(employee_details.Joining_Date.Value.ToString());
            decimal Salary1 = employee_details.Salary.Value;

            int tenure = (DateTime.Parse(DateTime.Now.ToString()) - Jd).Days / 30;
            decimal paidtillnow = (Salary1 * tenure);

            DateTime p1 = (c != null ? c.Po_start_Date.Value : DateTime.Now);
            DateTime p2 = (c != null ? c.Po_end_date.Value : DateTime.Now);
            int Ctenure = ((p2.Year - p1.Year) * 12) + p2.Month - p1.Month;
            int btenure = tenure - Ctenure;
            decimal bench_exp = Salary1;
            decimal bench_expenes = 0;
            if (_empbills != null)
            {
                bench_expenes = btenure * (_empbills.Food_cost.Value + _empbills.Transport_cost.Value + _empbills.Cubical_cost.Value);
            }

            decimal Clientsal = Ctenure * (c != null ? c.Billing.Value : 0);
            if (employee_details.Salary != null && employee_details.Salary.Value > 0 && tenure > 0)
            {
                employee_details.Paidtillnow = paidtillnow;
            }
            decimal Profit_loss = Clientsal - (paidtillnow + bench_expenes);
            employee_details.Profit_OR_loss = Profit_loss;
            employee_details.Tenure = tenure;
            employee_details.Bench_Tenure = btenure;
            employee_details.Bench_expences = bench_expenes; //employee salary on bench including expenes

            if (employee_details == null)
            {
                return HttpNotFound();
            }

            return View(employee_details);
        }

        // GET: Employee_details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee_details/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee_details employee_details)
        {
            if (ModelState.IsValid)
            {
                db.Employee_details.Add(employee_details);
                db.SaveChanges();
                return RedirectToAction("Index");
       
            }
           
            return View(employee_details);

        }

        // GET: Employee_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Employee_details employee_details = db.Employee_details.Find(id);
            if (employee_details==null)
            {
                return HttpNotFound();
            }
            return View(employee_details);
        }

        // POST: Employee_details/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee_details employee_details)
        {
          

            if (ModelState.IsValid)
            {

                db.Entry(employee_details).State = EntityState.Modified;
             
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee_details);
        }

        // GET: Employee_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_details employee_details = db.Employee_details.Find(id);
            if (employee_details == null)
            {
                return HttpNotFound();
            }
            return View(employee_details);
        }

        // POST: Employee_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee_details employee_details = db.Employee_details.Find(id);
            db.Employee_details.Remove(employee_details);
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
