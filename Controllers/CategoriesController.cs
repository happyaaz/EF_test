using LostAndFound.Helpers;
using LostAndFound.Models;
using LostAndFound.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LostAndFound.Controllers
{
    [Authorize(Roles = "Registrar,Admin")]
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            var db = new MainDbContext();
            var categories = db.deviceCategory.ToList();

            if (categories.Count > 0)
            {
                var lastId = categories.Max(c => c.deviceCategoryId);
                ViewBag.lastCategoryId = (lastId + 1);
            }
            else
            {
                ViewBag.lastCategoryId = 0;
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {

            var db = new MainDbContext();
            var neededCategory = db.deviceCategory.Find(id);

            if (neededCategory == null)
            {
                return HttpNotFound();
            }

            db.deviceCategory.Remove(neededCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCategory(int id, string newCategoryName)
        {
            var db = new MainDbContext();
            var neededCategory = db.deviceCategory.Find(id);

            if (neededCategory == null)
            {
                return HttpNotFound();
            }

            string oldCategoryName = neededCategory.deviceCategoryName;

            neededCategory.deviceCategoryName = newCategoryName;
            db.Entry(neededCategory).State = EntityState.Modified;

            db.SaveChanges();

            var devicesWithOldName = db.clientDeviceInfo.Where(d => d.deviceCategory == oldCategoryName).ToList();

            if (devicesWithOldName.Count > 0)
            {
                foreach (ClientDeviceInfo cdi in devicesWithOldName)
                {
                    cdi.deviceCategory = newCategoryName;
                    db.Entry(cdi).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(new { success = true, responseText = "Updated category"}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddNewCategory(string id, string categoryName)
        {

            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    DeviceCategory dc = new DeviceCategory();

                    int categoryId = Convert.ToInt32(id);

                    //dc.deviceCategoryId = categoryId;
                    dc.deviceCategoryName = categoryName;

                    db.deviceCategory.Add(dc);
                    db.SaveChanges();
                    //  also send an incremented ID
                    return Json(new { success = true, responseText = "Added category", newId = (categoryId + 1) }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = true, responseText = "Something went wrong" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}