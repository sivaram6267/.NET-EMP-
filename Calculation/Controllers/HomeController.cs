
using System;
using System.collections.generic;
using system.linq;
using system.web;
using system.web.mvc;
using System.Web.Mvc;
using Calculation.Models;

namespace calculation.controllers
{
    public class homecontroller : Controller
    {
        // get: home
        public ActionResult index()
        {
            return View(new Tenure());
        }
        [HttpPut]
        public ActionResult index(Tenure t)
        {
            t.tenure = ((DateTime.Now.Year - t.joining_date.Year) * 12) + DateTime.Now.Month - t.joining_date.Month;
            return View(t);
        }
    }
}