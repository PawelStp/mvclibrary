using MVCLibrary.Models;
using MVCLibrary.MyCart;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.Services
{
    public class CartServic
    {

        public CartBookViewModel GetCartViewModel()
        {
            var cartBookViewModel = new CartBookViewModel();
            using (var dbContext = new MvclibraryEntities())
            {
                var books = dbContext.Books.ToList();
                var bookvm = new List<BookViewModel>();
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
            return cartBookViewModel;
        }

    }
}