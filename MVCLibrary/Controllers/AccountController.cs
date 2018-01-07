using MVCLibrary.Models;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCLibrary.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel user)
        {
            try
            {
                // TODO: Add insert logic here
                string message = string.Empty;
                if (ModelState.IsValid)
                {
                    using (var dbContext = new MvclibraryEntities())
                    {
                        var userzy = dbContext.User.ToList();

                        var u = userzy.Where(pr => pr.Email == user.Email).FirstOrDefault();

                        if (u != null)
                        {
                            ModelState.AddModelError("UzytkownikIstnieje", "Podany email istnieje w bazie danych");
                            return View(user);
                        }

                        var newUser = new User
                        {
                            Email = user.Email,
                            Password = user.Password,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsUserVerified = false,
                            Role = "User",
                            BorrowLimit = 5,
                            WaitLimit = 5
                        };

                        dbContext.User.Add(newUser);
                        dbContext.SaveChanges();
                    }
                    ViewBag.Status = false;
                    ViewBag.Message = "Rejestracja zakończyła się powodzeniem!!! Logowanie będzie możliwe po zweryfikowaniu konta przez pracownika";
                    return View("Success");
                }
                ViewBag.Status = false;
                ViewBag.Message = "Coś poszło nie tak";
                return View(user);

            }
            catch
            {
                return View(user);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user, string ReturnUrl = "")
        {
            try
            {
                // TODO: Add insert logic here
                string message = string.Empty;

                using (var dbContext = new MvclibraryEntities())
                {
                    var userzy = dbContext.User.ToList();

                    var us = userzy.Where(pr => pr.Email == user.Email).FirstOrDefault();

                    if (us != null)
                    {
                        if (us.IsUserVerified == true)
                        {
                            if (string.Compare(us.Password, user.Password) == 0)
                            {
                                int timeout = 20;
                                var ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), true, String.Join("|", us.Role));
                                string encrypted = FormsAuthentication.Encrypt(ticket);
                                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                                cookie.HttpOnly = true;
                                Response.Cookies.Add(cookie);

                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                message = "Niepoprawne dane";
                            }
                        }
                        else
                        {
                            message = "Twoje konto nie zostało jeszcze zweryfikowane";
                        }
                    }
                    else
                    {
                        message = "Niepoprawne dane";
                    }
                }

                ViewBag.Message = message;

                return View();
            }
            catch
            {
                return View(user);
            }
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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
