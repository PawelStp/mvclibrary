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

        [Authorize]
        public ActionResult UserLEnd()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var userEmail = User.Identity.Name;
                var user = dbContext.User.Where(u => u.Email == userEmail).FirstOrDefault();

                var lends = dbContext.Lend.Where(l => l.UserId == user.Id ).ToList();

                var lendServicew = new LendService();

                var lendViewModel = lendServicew.GetViewModel(lends.OrderBy(o => o.DateBorrowed).ToList());

                return View(lendViewModel);
            }
        }

        [Authorize]
        public ActionResult UserLEnd2()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var userEmail = User.Identity.Name;
                var user = dbContext.User.Where(u => u.Email == userEmail).FirstOrDefault();

                var lends = dbContext.Lend.Where(l => l.UserId == user.Id && l.State == "Na półce czytelnika").ToList();

                var lendServicew = new LendService();

                var lendViewModel = lendServicew.GetViewModel(lends.OrderBy(o => o.DateBorrowed).ToList());

                return View("UserLend",lendViewModel);
            }
        }
    }
}
