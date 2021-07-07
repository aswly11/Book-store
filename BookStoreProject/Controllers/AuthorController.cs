using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNewAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewAuthor(Author a )
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            if(db.Authors.Find(a.AuthorID)==null)
            {
                db.Authors.Add(a);
                db.SaveChanges();
             }
            
            //what if he comes from addnew book
            
            return RedirectToAction("Home", "Admin");
        }
        public ActionResult EditAuthor(int? id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            return View(db.Authors.Find(id));
        }
        [HttpPost]
        public ActionResult EditAuthor(Author c, int? id)
        {

            dbbookstoreEntities db = new dbbookstoreEntities();
            Author c1 = db.Authors.Find(id);
            c1.AuthorName = c.AuthorName;
            db.Entry(c1).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DisplayAll", "Author");
        }
        public ActionResult DisplayAll()
        {
            return View();
        }
    }
}