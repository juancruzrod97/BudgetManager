using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetManager.Models;

namespace BudgetManager.Controllers
{
    public class OperationController : Controller
    {
        BM_DataBaseEntities db = new BM_DataBaseEntities();
        // GET: Operations
        public ActionResult Index()
        {
            var operations = db.Operation.ToList();
            return View(operations);
        }

        public ActionResult AddOperation()
        {
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(new SelectListItem { Text = "Income", Value = "1" });
            types.Add(new SelectListItem { Text = "Outcome", Value = "2" });
            ViewBag.Types = types;
            return View();
        }
        [HttpPost]
        public ActionResult AddOperation(Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Operation.Add(operation);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(operation);
        }
    }
}