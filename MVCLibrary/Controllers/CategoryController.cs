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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var categories = dbContext.category.ToList();
                var categoriesvm = new List<CategoryViewModel>();
                foreach (var item in categories)
                {
                    var rootName = string.Empty;

                    if (item.RootCategoryId != null)
                        rootName = categories.Where(c => c.Id == item.RootCategoryId).FirstOrDefault().Name;
                    categoriesvm.Add(new CategoryViewModel
                    {
                        
                        Name = item.Name,
                        RootName = rootName,
                        Id=item.Id
                    });
                }
                return View(categoriesvm);
            }
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin, Worker")]
        public ActionResult Create()
        {
         
            using (var dbContext = new MvclibraryEntities())
            {
                var s = dbContext.category.ToList();
                var zvm = new CategoryAddViewModel();

                zvm.CategoryList = s.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                });

                return View(zvm);
            }
        }

        // POST: Category/Create
        [Authorize(Roles = "Admin, Worker")]
        [HttpPost]
        public ActionResult Create(CategoryAddViewModel newCategory)
        {
            try
            {
                using (var dbContext = new MvclibraryEntities())
                {
                    int? rootId = null;
                    if (newCategory.SelectedCategoryId != null)
                        rootId = int.Parse(newCategory.SelectedCategoryId);
                    var category = new category
                    {
                        Name = newCategory.Name,
                        RootCategoryId = rootId
                    };
                    dbContext.Entry(category).State = EntityState.Added;
                    dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            using (var dbContext = new MvclibraryEntities())
            {
                var s = dbContext.category.ToList();
                var editCategory = dbContext.category.Where(c => c.Id == id).FirstOrDefault();

                var zvm = new CategoryAddViewModel
                {
                    Id=editCategory.Id,
                    Name = editCategory.Name,
                    SelectedCategoryId=editCategory.RootCategoryId.ToString()
                };

                zvm.CategoryList = s.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name

                });

                return View(zvm);
            }
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryAddViewModel c)
        {
            try
            {
                using (var dbContext = new MvclibraryEntities())
                {
                    var s = dbContext.category.ToList();
                    var editCategory = dbContext.category.Where(b => b.Id == c.Id).FirstOrDefault();

                    editCategory.Name = c.Name;
                    editCategory.RootCategoryId = c.Id;
                    dbContext.Entry(c).State = EntityState.Modified;
                    dbContext.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
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
