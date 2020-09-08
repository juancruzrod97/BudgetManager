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
        BudgetManager.Models.BM_DataBaseEntities db = new BM_DataBaseEntities();
        // GET: Operations
        public ActionResult Index()
        {
            if (Session["IdUser"] != null)
            {
                var operations = db.Operation.ToList().OrderByDescending(x=>x.Date).Take(10).ToList(); //chequear esto
                var count = db.Operation.Count();
                ViewBag.Balance = Balance();
                return View(operations);
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult AddOperation()
        {
            if (Session["IdUser"] != null)
            {
                ViewBag.Categories = new SelectList(db.Category, "IdCategory", "Name").OrderBy(x => x.Text);
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddOperation(Operation operation)
        {
            _ = int.TryParse(Session["IdUser"].ToString(), out int x);
            operation.IdUser = x;
            if (ModelState.IsValid)
            {
                db.Operation.Add(operation);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(operation);
        }
        public ActionResult EditOperation(int id)
        {
            var operation = db.Operation.Find(id);
            return View(operation);
        }
        [HttpPost]
        public ActionResult EditOperation(Operation operation)
        {
            if (Session["IdUser"] != null) //el objeto no se esta pasando correctamente, solo trae Concept y Value
            {
                if (ModelState.IsValid)
                {
                    db.Entry(operation).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(operation);
            }
            else return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteOperation(int Id)
        {
            var operation = db.Operation.Find(Id);
            db.Operation.Remove(operation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool Validation(Operation op)
        {
            bool result = false;
            return result;
        }

        public decimal Balance()
        {
            decimal total = 0;
            foreach(Operation op in db.Operation.ToList())
            {
                if (op.Category.Type == "Income")
                    total = total + op.Value;
                else
                    total = total - op.Value;
            }
            return total;
        }
    }
}