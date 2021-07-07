using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer c , FormCollection form)
        {
            var isadmin = Session["isAdmin"];
            if (isadmin != null)
            {
                c.isAdmin = true;
            }
            dbbookstoreEntities db = new dbbookstoreEntities();
            if(c.CustomerPassword!=form["ConfirmPassword"].ToString())
            {
                ViewBag.confirmisfalse = true;
                return View();
            }
            Customer user = db.Customers.Where(x => x.CustomerEmail == c.CustomerEmail).FirstOrDefault();
            if(user==null)
            {

                db.Customers.Add(c);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");

            }
            else
            {
                ViewBag.isexist = true;
                return View();
            }
       
        }
        public ActionResult Home()
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            var categories = db.Categories.ToList();
            var books = db.Books.ToList();
            ViewBag.categories = categories;
            ViewBag.books = books;
            return View();

        }
    }
}