using MVCLibrary.Models;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            using (var dbContext = new MvclibraryEntities())
            {
                var books = dbContext.Books.OrderByDescending(b => b.DateCreatead).Take(5).ToList();
                var bookViewModel = new List<BookViewModel>();
                foreach (var item in books)
                {
                    bookViewModel.Add(new BookViewModel
                    {
                        Author = item.Author,
                        ISBN = item.ISBN,
                        status = item.Status,
                        NameCategory = dbContext.category.Where(c => c.Id == item.CategoryId).FirstOrDefault().Name,
                        Title = item.Title,
                        Id = item.Id
                    });
                }

                var messages = dbContext.message.OrderByDescending(b => b.DateCreated).Take(3).ToList();
                homeViewModel.messages = messages;
                homeViewModel.bookViewModel = bookViewModel;

            }
            return View(homeViewModel);
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