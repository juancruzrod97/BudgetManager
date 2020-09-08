using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetManager.Models;
namespace BudgetManager.Controllers
{
    public class HomeController : Controller
    {
        BM_DataBaseEntities db = new BM_DataBaseEntities(); 
        // GET: Home
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserProfile user)
        {
            UserProfile login = ValidLogin(user);
            if (login != null|| Session.Count >0)
            {
                
                Session["IdUser"] = login.IdUser;
                Session["FullName"] = login.FullName;
                return RedirectToAction("Index", "Operation");
            }
            return View(user);
        }

        private UserProfile ValidLogin(UserProfile user)
        {
            UserProfile result = null;
            if (db.UserProfile.Any(x => x.Email == user.Email && x.Password == user.Password))
                result = db.UserProfile.First(x => x.Email == user.Email && x.Password == user.Password);
             return result;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}