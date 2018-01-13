using MVCLibrary.Models;
using MVCLibrary.MyCart;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        [Authorize]
        public ActionResult Index(Cart cart)
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var histroy = dbContext.History.ToList();

                var user = dbContext.User.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
                var userHistory = histroy.Where(h => h.UserID == user.Id).ToList();

                var bookvm = new List<BookViewModel>();
                var bookse = dbContext.Books.ToList();

                var cartBookViewModel = new CartBookViewModel();
                cartBookViewModel.cart = cart;

                foreach (var h in userHistory)
                {
                    if (h.Word == null) h.Word = "";
                   var books= bookse.Where(b => b.ISBN.Contains(h.Word) || b.Title.Contains(h.Word) || b.Author.Contains(h.Word)).ToList();
                    foreach (var item in books)
                    {
                        cartBookViewModel.BookViewModels.Add(new BookViewModel
                        {
                            Author = item.Author,
                            ISBN = item.ISBN,
                            status = item.Status,
                            NameCategory = dbContext.category.Where(c => c.Id == item.CategoryId).FirstOrDefault().Name,
                            Title = item.Title,
                            Id = item.Id
                        });
                    }

                }

                return View(cartBookViewModel);
            }
        }
    }
}