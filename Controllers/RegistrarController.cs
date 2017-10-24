using LostAndFound.Helpers;
using LostAndFound.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LostAndFound.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_REGISTRAR)]
    public class RegistrarController : Controller
    {
        
        public ActionResult Index()
        {
            return RedirectToAction("RegisteredDevices", "Device");
        }
    }
}