using MVCLibrary.Models;
using MVCLibrary.MyCart;
using MVCLibrary.Services;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index(Cart cart)
        {
            var cartBookViewModel = new CartBookViewModel();
            cartBookViewModel.cart = cart;
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
                return View(cartBookViewModel);
            }
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var s = dbContext.category.ToList();
                var zvm = new BookAddViewModel();

                zvm.CategoryList = s.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                });

                return View(zvm);
            }
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(BookAddViewModel newBook)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbContext = new MvclibraryEntities())
                    {
                        if (dbContext.Books.Where(b => b.ISBN == newBook.ISBN).FirstOrDefault() != null)
                        {
                            ModelState.AddModelError("BookExist", "Książka o podanym ISBN juz istnieje");

                            var s = dbContext.category.ToList();

                            newBook.CategoryList = s.Select(x => new SelectListItem
                            {
                                Value = x.Id.ToString(),
                                Text = x.Name

                            });
                            return View(newBook);
                        }
                        var book = new Books
                        {
                            Author = newBook.Author,
                            Status = "Dostepna",
                            Title = newBook.Title,
                            ISBN = newBook.ISBN,
                            CategoryId = int.Parse(newBook.SelectedCategoryId),
                            DateCreatead = DateTime.Now
                        };
                        dbContext.Entry(book).State = EntityState.Added;
                        dbContext.SaveChanges();

                        return RedirectToAction("Index");
                    }

                }

                using (var dbContext = new MvclibraryEntities())
                {
                    var s = dbContext.category.ToList();

                    newBook.CategoryList = s.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name

                    });
                }
                return View(newBook);
            }
            catch
            {
                return View(newBook);
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var b = dbContext.Books.ToList();
                var editBooks = dbContext.Books.Where(c => c.Id == id).FirstOrDefault();

                var zvm = new BookAddViewModel
                {
                    Id = editBooks.Id,
                    SelectedCategoryId = editBooks.CategoryId.ToString(),
                    Author = editBooks.Author,
                    ISBN = editBooks.ISBN,
                    Title = editBooks.Title
                };

                zvm.CategoryList = dbContext.category.ToList().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                });

                return View(zvm);
            }
        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(BookAddViewModel bvm)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    using (var dbContext = new MvclibraryEntities())
                    {
                        var books = dbContext.Books.ToList();
                        if (books.Where(b => b.ISBN == bvm.ISBN && b.Id != bvm.Id).FirstOrDefault() != null)
                        {
                            ModelState.AddModelError("BookExist", "Książka o podanym ISBN juz istnieje");
                            bvm.CategoryList = dbContext.category.ToList().Select(x => new SelectListItem
                            {
                                Value = x.Id.ToString(),
                                Text = x.Name

                            });
                            return View(bvm);
                        }
                        var editBook = dbContext.Books.Where(b => b.Id == bvm.Id).FirstOrDefault();

                        editBook.ISBN = bvm.ISBN;
                        editBook.Title = bvm.Title;
                        editBook.Author = bvm.Author;
                        editBook.CategoryId = int.Parse(bvm.SelectedCategoryId);
                        dbContext.Entry(editBook).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                using (var dbContext = new MvclibraryEntities())
                {

                    bvm.CategoryList = dbContext.category.ToList().Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name

                    });

                    return View(bvm);
                }
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult AddBookToCart(Cart cart, int id)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            CartServic cartService = new CartServic();
            var cartViewModel = cartService.GetCartViewModel();
            if (cart.books.Count < 5)
            {
                if (cart.books.Find(b => b.Id == id) == null)
                {
                    using (var dbContext = new MvclibraryEntities())
                    {

                        var books = dbContext.Books.ToList();

                        var book = books.Where(b => b.Id == id).FirstOrDefault();
                        cart.books.Add(book);

                    }
                }
                else
                {
                    TempData["Message"] = "Podana książka znajduje się już w koszyku";
                    TempData["Status"] = "false";
                }
            }
            else
            {
                TempData["Message"] = "Nie można przekroczyć 5 książek w koszyku";
                TempData["Status"] = "false";
            }
            cartViewModel.cart = cart;
            return Redirect(url);
        }
        [Authorize]
        public ActionResult RemoveBook(Cart cart, int id)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            CartServic cartService = new CartServic();
            var cartViewModel = cartService.GetCartViewModel();

            var book = cart.books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                cart.books.Remove(book);
            }

            cartViewModel.cart = cart;
            return Redirect(url);

        }
        [Authorize]
        public ActionResult ClearCart(Cart cart)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            CartServic cartService = new CartServic();
            var cartViewModel = cartService.GetCartViewModel();
            cart.books.Clear();
            cartViewModel.cart = cart;
            return Redirect(url);

        }

        [Authorize]
        public ActionResult LendBooks(Cart cart)
        {
            string url = this.Request.UrlReferrer.AbsolutePath;

            CartServic cartService = new CartServic();
            var cartViewModel = cartService.GetCartViewModel();


            var userEmail = User.Identity.Name;

            using (var dbContext = new MvclibraryEntities())
            {
                var user = dbContext.User.Where(u => u.Email == userEmail).FirstOrDefault();

                var lends = dbContext.Lend.ToList();

                var userLends = lends.Where(l => l.UserId == user.Id && (l.State == "Wypożyczona" || l.State == "Na półce czytelnika")).ToList();

                if (userLends.Count + cart.books.Count <= user.BorrowLimit)
                {
                    foreach (var item in cart.books)
                    {
                        var lend = new Lend
                        {
                            BookId = item.Id,
                            UserId = user.Id,
                            DateBorrowed = DateTime.Now,
                            State = "Na półce czytelnika",
                            DateReturn = DateTime.Now.AddDays(14)
                        };
                        item.Status = "Wypożyczona";

                        dbContext.Entry(item).State = EntityState.Modified;
                        dbContext.Lend.Add(lend);
                    }
                    dbContext.SaveChanges();

                    TempData["Message"] = "Książki czekają na Twój odbiór";
                    TempData["Status"] = "true";
                }
                else
                {
                    TempData["Message"] = "Masz już wypożyczone " + userLends.Count + " ksiązki. Twój limit wynosi " + user.BorrowLimit;
                    TempData["Status"] = "false";
                }

            }

            cart.books.Clear();
            cartViewModel.cart = cart;
            return Redirect(url);

        }

        public ActionResult Search(Cart cart, string value)
        {
            var cartViewModel = new CartBookViewModel();
            using (var dbContext = new MvclibraryEntities())
            {
                List<Books> books = null;
                if(value!=null)books =  dbContext.Books.Where(b => b.ISBN.Contains(value) || b.Title.Contains(value) || b.Author.Contains(value)).ToList();
                else
                {
                    books = dbContext.Books.ToList();
                }

                var bookvm = new List<BookViewModel>();
                foreach (var item in books)
                {
                    cartViewModel.BookViewModels.Add(new BookViewModel
                    {
                        Author = item.Author,
                        ISBN = item.ISBN,
                        status = item.Status,
                        NameCategory = dbContext.category.Where(c => c.Id == item.Id).FirstOrDefault().Name,
                        Title = item.Title,
                        Id = item.Id
                    });
                }
                cartViewModel.cart = cart;
                return View("Index", cartViewModel);
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Books/Delete/5
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
