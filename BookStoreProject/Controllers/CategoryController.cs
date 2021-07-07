using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewCatgory(int?id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            Category b = db.Categories.Find(id);
            if (b == null)
                return HttpNotFound();

            return View(b);
        }
        
        [HttpGet]
        public ActionResult AddNewCatgory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewCatgory(Category c)
        {

            dbbookstoreEntities db = new dbbookstoreEntities();
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Home","Admin");
        }
        public ActionResult EditCatgory(int?id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();


            return View(db.Categories.Find(id));
        }
        [HttpPost]
        public ActionResult EditCatgory(Category c,int?id)
        {

            dbbookstoreEntities db = new dbbookstoreEntities();
            Category c1 = db.Categories.Find(id);
            c1.CategoryName = c.CategoryName;
            c1.CategoryDescription = c.CategoryDescription;
            db.Entry(c1).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DisplayAll", "Category");
        }
        public ActionResult DisplayAll()
        {
            return View();
        }
    }
}