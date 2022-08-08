using login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult create()
        {
            Emp_Entities db = new Emp_Entities();
           List<Employee_details> li = db.Employee_details.ToList();
            ViewBag.Emp_list = new SelectList(li, "Emp_Id","Emp_Name");

            return View();
        }
        [HttpPost]
        public ActionResult create(Employeeviewmodel evm)
        {
            try
            {
                Emp_Entities db = new Emp_Entities();
                List<Employee_details> li = db.Employee_details.ToList();
                ViewBag.Emp_list = new SelectList(li, "Emp_Id","Emp_Name");
                Client_Details cd = new Client_Details();
               
             
                cd.Client_Name = evm.Client_Name;
                cd.Po_end_date = evm.Po_end_date;
                cd.Po_start_Date = evm.Po_start_Date;
                cd.Desination_at_client = evm.Desination_at_client;
                cd.Billing = evm.Billing;
                int latestEmpId = evm.Emp_Id;
                cd.Emp_id_fk = latestEmpId;
                db.Client_Details.Add(cd);
                db.SaveChanges();
                Bill b = new Bill();
                b.Cubical_cost = evm.Cubical_cost;
                b.Food_cost = evm.Food_cost;
                b.Transport_cost = evm.Transport_cost;
                b.Emp_Id_FK = latestEmpId;
                db.Bills.Add(b);
                db.SaveChanges();
                return RedirectToAction("create");
            }
            catch(Exception)
            {
                
            }
            return View();
        }
    }
}