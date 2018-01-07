using MVCLibrary.Models;
using MVCLibrary.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class LendController : Controller
    {
        // GET: Lend
        public ActionResult Index()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var lends = dbContext.Lend.ToList();

                var lendServicew = new LendService();

                var lendViewModel = lendServicew.GetViewModel(lends.OrderBy(o => o.DateBorrowed).ToList());
                return View(lendViewModel);
            }
        }

        public ActionResult WaitingBooks()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var lends = dbContext.Lend.Where(l => l.State == "Na półce czytelnika").ToList();

                var lendServicew = new LendService();

                var lendViewModel = lendServicew.GetViewModel(lends.OrderBy(o => o.DateBorrowed).ToList());
                return View("Index", lendViewModel);
            }
        }

        public ActionResult Lend()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var lends = dbContext.Lend.Where(l => l.State == "Odebrano").ToList();

                var lendServicew = new LendService();

                var lendViewModel = lendServicew.GetViewModel(lends.OrderBy(o => o.DateBorrowed).ToList());
                return View("Index", lendViewModel);
            }
        }

        public ActionResult Borrow(int id)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            using (var dbContext = new MvclibraryEntities())
            {
                var lend = dbContext.Lend.Where(l => l.Id == id).FirstOrDefault();

                lend.State = "Odebrano";
                dbContext.Entry(lend).State = EntityState.Modified;
                dbContext.SaveChanges();
                return Redirect(url);
            }
        }
        public ActionResult Return(int id)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;
            using (var dbContext = new MvclibraryEntities())
            {
                var lend = dbContext.Lend.Where(l => l.Id == id).FirstOrDefault();

                lend.State = "Zwrócono";
                dbContext.Entry(lend).State = EntityState.Modified;

                var book = dbContext.Books.Where(b => b.Id == lend.BookId).FirstOrDefault();
                book.Status = "Dostepna";
                dbContext.Entry(book).State = EntityState.Modified;

                dbContext.SaveChanges();
                return Redirect(url);
            }
        }
        // GET: Lend/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lend/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lend/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lend/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lend/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lend/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lend/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
