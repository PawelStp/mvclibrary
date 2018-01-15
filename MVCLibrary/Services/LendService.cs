using MVCLibrary.Models;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLibrary.Services
{
    public class LendService
    {

        public List<LendViewModel> GetViewModel(List<Lend> lends)
        {
            var lendViewModel = new List<LendViewModel>();

            using (var dbContext = new MvclibraryEntities())
            {
                var books = dbContext.Books.ToList();

                var users = dbContext.User.ToList();

                foreach (var item in lends)
                {
                    var book = books.Where(b => b.Id == item.BookId).FirstOrDefault();
                    var user = users.Where(u => u.Id == item.UserId).FirstOrDefault();
                    lendViewModel.Add(
                        new LendViewModel
                        {
                            Id=item.Id,
                            Author = book.Author,
                            DateBorrowed = item.DateBorrowed ?? DateTime.Now,
                            DateReturn = item.DateReturn ?? DateTime.Now.AddDays(14),
                            State = item.State,
                            Surname = user.LastName,
                            Name = user.FirstName,
                            ISBN = book.ISBN,
                            Title = book.Title,
                            Email = user.Email

                        }
                    );
                }
            }
            return lendViewModel;
        }
    }
}