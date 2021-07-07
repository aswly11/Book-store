using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddNewBook()
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            List<Category> c = db.Categories.ToList();
            List<string> catNames = new List<string>();
            foreach (Category i in c)
            {
                catNames.Add(i.CategoryName);
            }
            List<Author> Author = db.Authors.ToList();
            List<string> AuthorsNames = new List<string>();
            foreach (Author i in Author)
            {
                AuthorsNames.Add(i.AuthorName);
            }
            ViewBag.names = catNames;
            ViewBag.authornmes = AuthorsNames;
            return View();

        }
        [HttpPost]
        public ActionResult AddNewBook( Book b1 , HttpPostedFileBase image,FormCollection form)
        {
            if (image != null)
            {
                b1.BookPicture = new byte[image.ContentLength];
                image.InputStream.Read(b1.BookPicture, 0, image.ContentLength);

                dbbookstoreEntities db = new dbbookstoreEntities();
                string authorname = form["Authors"].ToString();
                Author a1 = db.Authors.Where(x => x.AuthorName.Contains(authorname)).FirstOrDefault();
                b1.AuthorID = a1.AuthorID;
                string catName = form["Categories"].ToString();
                Category c1 = db.Categories.Where(x => x.CategoryName ==catName ).SingleOrDefault();
                b1.CategoryID = c1.CategoryID;
                b1.BookViews = 0;
                db.Books.Add(b1);
                db.SaveChanges();
                return RedirectToAction("Home","Admin");
            }
            return View();
        }
        public ActionResult ViewBook(int? id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            Book b = db.Books.Find(id);
            if (id == null)
                return HttpNotFound();
          

            return View(b);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();

            return View(b);
        }
     
        public ActionResult Delete(int? id)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();

            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Home", "Admin");
        }
       
      
        [HttpPost]
        public ActionResult Edit(Book b1,int? id,HttpPostedFileBase image, FormCollection form)
        {
            dbbookstoreEntities db = new dbbookstoreEntities();
            Book b2 = db.Books.Find(id);
            if (image != null)
            {

                b1.BookPicture = new byte[image.ContentLength];
                image.InputStream.Read(b1.BookPicture, 0, image.ContentLength);
                b2.BookPicture = b1.BookPicture;
            }
           
            string authorname = form["Authors"].ToString();
                Author a1 = db.Authors.Where(x => x.AuthorName == authorname).FirstOrDefault();
                b2.AuthorID = a1.AuthorID;
                b2.Author = a1;                         
             
           
                string catName = form["Categories"].ToString();
                Category c1 = db.Categories.Where(x => x.CategoryName == catName).SingleOrDefault();
                b2.CategoryID = c1.CategoryID;
                b2.Category = c1;
                b2.BookName = b1.BookName;
                 b2.BookDescription = b1.BookDescription;
                  b2.BookPrice = b1.BookPrice;
            b2.downloadLink = b1.downloadLink;
                if (ModelState.IsValid)
                {
                    //db.Books.Attach(b1);
                    db.Entry(b2).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ViewBook", new { id = b2.BookID });

                }
            
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest); ;
        }

    }
}