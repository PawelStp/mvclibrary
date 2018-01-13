using MVCLibrary.Models;
using MVCLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLibrary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles = "Admin, Worker")]
        public ActionResult Index()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var users = dbContext.User.Where(u => u.Role == "User").ToList();
                return View(users);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Workers()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var workers = dbContext.User.Where(u => u.Role == "Worker").ToList();
                return View("Index", workers);
            }
        }
        [Authorize(Roles = "Admin, Worker")]
        public ActionResult UsersToVerify()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var users = dbContext.User.Where(u => u.Role == "User" && u.IsUserVerified == false).ToList();
                return View(users);
            }

        }

        [Authorize(Roles = "Admin, Worker")]
        public ActionResult Verify(int id)
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var us = dbContext.User.Where(u => u.Id==id).FirstOrDefault();
                us.IsUserVerified = true;
                dbContext.Entry(us).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("UsersToVerify");
            }

        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {

          
            using (var dbContext = new MvclibraryEntities())
            {
                var us = dbContext.User.Where(u => u.Id == id).FirstOrDefault();
                EditUserViewModel zvm = new EditUserViewModel()
                {
                    Id = id,
                    BorrowLimit=us.BorrowLimit,
                    Email=us.Email,
                    FirstName=us.FirstName,
                    LastName=us.LastName,
                    WaitLimit=us.WaitLimit,
                    Role=us.Role
                };
                Dictionary<string, string> Role = new Dictionary<string, string>();
                Role.Add("Worker", "Worker");
                Role.Add("User", "User");
                zvm.Rols = Role.Select(x => new SelectListItem
                {
                    Value = x.Key,
                    Text = x.Value
                });
                return View(zvm);

            }
        }
        [Authorize]
        public ActionResult Profile()
        {
            return View();
        }
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(EditUserViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbContext = new MvclibraryEntities())
                    {
                        var us = dbContext.User.Where(u => u.Id == collection.Id).FirstOrDefault();
                        us.FirstName = collection.FirstName;
                        us.LastName = collection.LastName;
                        us.Email = collection.Email;
                        us.Role = collection.Role;
                        us.BorrowLimit = collection.BorrowLimit;
                        us.WaitLimit = collection.WaitLimit;
                        dbContext.Entry(us).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                Dictionary<string, string> Role = new Dictionary<string, string>();
                Role.Add("Worker", "Worker");
                Role.Add("User", "User");
                collection.Rols = Role.Select(x => new SelectListItem
                {
                    Value = x.Key,
                    Text = x.Value
                });
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
