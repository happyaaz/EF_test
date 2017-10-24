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
    [Authorize(Roles = RoleNames.ROLE_ADMIN)]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Devices()
        {
            HttpCookie aCookie = new HttpCookie("numberOfDevicesPerPage");
            aCookie.Value = GetSharedVariables.ReturnInitialNumberOfDevicesPerPage ().ToString();
            aCookie.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(aCookie);

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisteredClients()
        {
            var db = new MainDbContext();


            return View(db.clientBasicInfo.ToList());
        }

        [HttpGet]
        public ActionResult DeleteClient(int id)
        {

            var db = new MainDbContext();
            var neededClient = db.clientBasicInfo.Find(id);

            if (neededClient == null)
            {
                return HttpNotFound();
            }

            db.clientBasicInfo.Remove(neededClient);
            db.SaveChanges();
            //  in case editing and shit fucks up
            TempData.Put("currentPageNumberForServerSidePagination", "0");
            TempData.Put("currentPageNumber", "0");
            return RedirectToAction("RegisteredClients");
        }

        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var db = new MainDbContext();
            var neededClient = db.clientBasicInfo.Find(id);

            if (neededClient == null)
            {
                return HttpNotFound();
            }
            TempData.Put("clientIdToEdit", neededClient.clientId.ToString());
            return View(neededClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditClient(ClientBasicInfo cbi_cl)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    //var _cbi_cl = db.clientBasicInfo.SingleOrDefault(c => c.clientId == cbi_cl.clientId);
                    //_cbi_cl = cbi_cl;
                    cbi_cl.clientId = Convert.ToInt32 (TempData.Get<string>("clientIdToEdit"));
                    db.Entry(cbi_cl).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            else
            {
                ModelState.AddModelError("", "Incorrect data has been entered");
            }

            //  in case editing and shit fucks up
            TempData.Put("currentPageNumberForServerSidePagination", "0");
            TempData.Put("currentPageNumber", "0");
            return RedirectToAction("RegisteredClients");
        }
    }
}