using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Home(int? id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            var categories = db.Categories.ToList();
            var books = db.Books.ToList();
            ViewBag.categories = categories;
            ViewBag.books = books;
            ViewBag.categoryFilter = id;
       
            return View();
        }


        public ActionResult logout()
        {
            Session["isAdmin"] = false;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult AddNewAdmin()
        {
           Session["isAdmin"]= true;
            return RedirectToAction("Register", "Customer");
        }

        public ActionResult check(int? id)
        {

            ViewBag.categoryFilter = id;
           return RedirectToAction("Home",new { id = ViewBag.categoryFilter });
         
        }
    }
}