using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form )
        {
            string username = form["Username"];
            string password = form["Password"];
            dbbookstoreEntities db = new dbbookstoreEntities();
            Customer admin = db.Customers.Where(x => x.CustomerUsename.Equals( username) && x.CustomerPassword.Equals(password) &&x.isAdmin==true).FirstOrDefault();
            Customer user = db.Customers.Where(x => x.CustomerUsename .Equals( username) && x.CustomerPassword.Equals(password) && x.isAdmin == false).FirstOrDefault();

            if (admin == null&&user==null)
            {
                ViewBag.state = false;
                return View();
            }
            else
            {
                if (admin != null)
                {
                    Session["isAdmin"] = true;
                    return RedirectToAction("Home", "Admin");

                    
                }
                else
                {
                    Session["isAdmin"] = false;
                    return RedirectToAction("Home", "Admin");

                }
                   
            }
        

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}