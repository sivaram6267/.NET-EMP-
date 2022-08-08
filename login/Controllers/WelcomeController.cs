using login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace login.Controllers
{
    [Authorize]
    public class WelcomeController : Controller
    {
        public Emp_Entities db = new Emp_Entities();
        
        [HttpGet]
        public ActionResult WelcomePage()
        {
            return View(db.Employee_details.ToList());

        }
    }
}

//FormsAuthentication.SignOut();
//return RedirectToAction("Login");