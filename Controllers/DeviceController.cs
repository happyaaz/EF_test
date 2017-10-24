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
    [Authorize(Roles = "Registrar,Admin")]
    public class DeviceController : Controller
    {

        int numberOfDevicesPerPageInitially = 10;

        //  to enable/disable pagination buttons
        [HttpGet]
        public ActionResult GetPaginatorValues()
        {
            int userId = GetCurrentClaimValues.GetCurrentUserId();

            var db = new MainDbContext();
            var listOfDevices = db.clientDeviceInfo.Where(d => d.deviceAddedByUser == userId).ToList();
            int total = listOfDevices.Count;

            double currentPage = Convert.ToDouble(TempData.Get<string>("currentPageNumberForServerSidePagination"));

            int numberOfDevicesPerPage = Convert.ToInt32(Request.Cookies["numberOfDevicesPerPage"].Value);

            int maxPages = (int)Math.Ceiling((double)total / numberOfDevicesPerPage);
            return Json(new
            {
                success = true,
                currentPage = Convert.ToInt32(TempData.Get<string>("currentPageNumberForServerSidePagination")),
                maxPages = maxPages,
                numberOfDevicesPerPage = numberOfDevicesPerPage,
                totalNumberOfDevicesRegisteredByUser = total
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        //  ideally, it should be a cookie.
        public ActionResult ChangeNumberOfItemsPerPage(int newNumberOfDevicesPerPage)
        {
            numberOfDevicesPerPageInitially = newNumberOfDevicesPerPage;

            if (Request.Cookies["numberOfDevicesPerPage"] != null)
            {
                //Request.Cookies["numberOfDevicesPerPage"].Value = newNumberOfDevicesPerPage.ToString();
                HttpCookie aCookie = new HttpCookie("numberOfDevicesPerPage");
                aCookie.Value = newNumberOfDevicesPerPage.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.SetCookie(aCookie);
            }

            TempData.Put("currentPageNumberForServerSidePagination", "0");
            TempData.Put("currentPageNumber", "0");

            return Json(new
            {
                success = true
            }
            , JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult RegisteredDevices()
        {
            if (Request.Cookies["numberOfDevicesPerPage"] == null)
            {
                //TempData.Put("numberOfDevicesPerPage", numberOfDevicesPerPageInitially.ToString());
                HttpCookie aCookie = new HttpCookie("numberOfDevicesPerPage");
                aCookie.Value = numberOfDevicesPerPageInitially.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.SetCookie(aCookie);
            }
            int userId = GetCurrentClaimValues.GetCurrentUserId();

            var db = new MainDbContext();

            int numberOfDevicesPerPage = Convert.ToInt32(Request.Cookies["numberOfDevicesPerPage"].Value);

            List<ClientDeviceInfo> listOfDevices = new List<ClientDeviceInfo>();
            string paginationMove_str = TempData.Get<string>("paginationMove_str");
            if (paginationMove_str == string.Empty || paginationMove_str == null)
            {
                listOfDevices = db.clientDeviceInfo.Where(d => d.deviceAddedByUser == userId).OrderByDescending(d => d.deviceRegistrationDate).
                    ThenByDescending(d => d.deviceRegistrationTime).Take(numberOfDevicesPerPage).ToList();
                TempData.Put("currentPageNumber", "0");
            }
            else
            {
                if (paginationMove_str == "previousDevices")
                {
                    int currentPage = Convert.ToInt32(TempData.Get<string>("currentPageNumber")) - 1;
                    //  just in case
                    if (currentPage < 0)
                    {
                        currentPage = 0;
                    }
                    listOfDevices = db.clientDeviceInfo.Where(d => d.deviceAddedByUser == userId).OrderByDescending(d => d.deviceRegistrationDate)
                        .ThenByDescending(d => d.deviceRegistrationTime).Skip(currentPage * numberOfDevicesPerPage)
                        .Take(numberOfDevicesPerPage).ToList();
                    TempData.Put("currentPageNumber", currentPage.ToString());

                    TempData.Put("currentPageNumberForServerSidePagination", currentPage.ToString());
                }
                else if (paginationMove_str == "nextDevices")
                {
                    int currentPage = Convert.ToInt32(TempData.Get<string>("currentPageNumber"));
                    currentPage++;
                    listOfDevices = db.clientDeviceInfo.Where(d => d.deviceAddedByUser == userId).OrderByDescending(d => d.deviceRegistrationDate)
                        .ThenByDescending(d => d.deviceRegistrationTime).Skip(currentPage * numberOfDevicesPerPage)
                        .Take(numberOfDevicesPerPage).ToList();
                    TempData.Put("currentPageNumber", currentPage.ToString());

                    TempData.Put("currentPageNumberForServerSidePagination", currentPage.ToString());
                }
            }

            foreach (ClientDeviceInfo cdi in listOfDevices)
            {
                string deviceRegistrationTime = cdi.deviceRegistrationTime;

                if (!deviceRegistrationTime.Contains("AM") && !deviceRegistrationTime.Contains("PM"))
                {
                    //  since we write the time in 24H format, we need to display it with AM/PM for the customers
                    string[] timeSplit = deviceRegistrationTime.Split(':');
                    string hour = timeSplit[0];
                    string min = timeSplit[1];
                    string sec = timeSplit[2];

                    string newHour = string.Empty;
                    string postfix = string.Empty;

                    int hour_int = Convert.ToInt32(hour);
                    if (hour_int > 12)
                    {
                        newHour = (hour_int - 12).ToString();
                        postfix = "PM";
                    }
                    else
                    {
                        newHour = hour_int.ToString();
                        postfix = "AM";
                    }

                    string formattedTime = newHour + ":" + min + ":" + sec + " " + postfix;
                    cdi.deviceRegistrationTime = formattedTime;
                }
            }

            return View(listOfDevices);
        }


        [HttpPost]
        public ActionResult PaginatedRegisteredDevices(string callCafeFrom_str)
        {
            TempData.Put("paginationMove_str", callCafeFrom_str);
            return Json(new { success = true, responseText = "Now can reload" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddNewDevice()
        {
            SaveCategoriesAndSubcategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewDevice(ClientDeviceInfo cdi_cl)
        {

            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    string timeToday = DateTime.Now.ToString("HH:mm:ss");
                    string dateToday = DateTime.Now.ToString("M/dd/yyyy");
                    string client = GetCurrentClaimValues.GetCurrentUserClient();
                    int deviceAddedByUser = GetCurrentClaimValues.GetCurrentUserId();

                    var device = db.clientDeviceInfo.Create();

                    device.client = client;
                    device.deviceAddedByUser = deviceAddedByUser;
                    device.deviceRegistrationDate = dateToday;
                    device.deviceRegistrationTime = timeToday;

                    //  since it is an id for the purposes of cascading dropdowns
                    //int deviceCategoryId = Convert.ToInt32(cdi_cl.deviceCategory);
                    //var materializeDeviceCategory = db.deviceCategory.Where(c => c.deviceCategoryId == deviceCategoryId).Select(c => c.deviceCategoryName).ToList();
                    string deviceCategory = cdi_cl.deviceCategory;

                    device.deviceCategory = deviceCategory;
                    //device.deviceSubcategory = cdi_cl.deviceSubcategory;
                    device.deviceName = cdi_cl.deviceName;
                    device.deviceNUINTASerialNumber = cdi_cl.deviceNUINTASerialNumber;
                    device.deviceCarrier = cdi_cl.deviceCarrier;
                    device.deviceCondition = cdi_cl.deviceCondition;
                    device.deviceBin = cdi_cl.deviceBin;
                    device.deviceRepairCost = cdi_cl.deviceRepairCost;
                    device.deviceOtherComments = cdi_cl.deviceOtherComments;

                    //  since an admin would have a slightly different view when registering a device
                    if (GetCurrentClaimValues.GetCurrentUserRole() == RoleNames.ROLE_ADMIN)
                    {
                        device.deviceUsedBy = cdi_cl.deviceUsedBy;
                        device.deviceValue = cdi_cl.deviceValue;
                        device.deviceSoldFor = cdi_cl.deviceSoldFor;
                    }

                    //Console.WriteLine(device);
                    db.clientDeviceInfo.Add(device);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect data has been entered");
            }

            return RedirectToAction("RegisteredDevices");

            //if (GetCurrentClaimValues.GetCurrentUserRole() == RoleNames.ROLE_ADMIN)
            //{
            //    return RedirectToAction("Devices", "Admin");
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Registrar");
            //}
        }



        [HttpPost]
        public ActionResult GetSubcategories(int categoryId)
        {
            List<string> relatedSubcategories_list = new List<string>();
            using (var db = new MainDbContext())
            {
                relatedSubcategories_list = db.deviceSubcategory.Where(s => s.deviceCategory.deviceCategoryId == categoryId)
                    .Select(s => s.deviceSubcategoryName).ToList();
            }
            return Json(new { subcategories = relatedSubcategories_list });
        }


        [HttpGet]
        public ActionResult EditDevice(int id)
        {
            var db = new MainDbContext();
            var neededDevice = db.clientDeviceInfo.Find(id);

            if (neededDevice == null)
            {
                return HttpNotFound();
            }
            //  for some reasons both of them get lost when not changed and sent to EditDevice, so need to store them just in case.
            TempData.Put("deviceCategory", neededDevice.deviceCategory);
            //TempData.Put("deviceSubcategory", neededDevice.deviceSubcategory);

            SaveCategoriesAndSubcategories();
            return View(neededDevice);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditDevice(ClientDeviceInfo cdi_cl)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    if (cdi_cl.deviceCategory == null)
                    {
                        cdi_cl.deviceCategory = TempData.Get<string>("deviceCategory");
                    }
                    /*
                    if (cdi_cl.deviceSubcategory == null)
                    {
                        cdi_cl.deviceSubcategory = TempData.Get<string>("deviceSubcategory");
                    }
                    */

                    string timeToday = DateTime.Now.ToString("hh:mm:ss tt");
                    string dateToday = DateTime.Now.ToString("M/dd/yyyy");

                    //  even though it's a edit date, still keep it as a registration date
                    cdi_cl.deviceRegistrationDate = dateToday;
                    cdi_cl.deviceRegistrationTime = timeToday;

                    string client = GetCurrentClaimValues.GetCurrentUserClient();
                    int deviceAddedByUser = GetCurrentClaimValues.GetCurrentUserId();

                    cdi_cl.client = client;
                    cdi_cl.deviceAddedByUser = deviceAddedByUser;

                    db.Entry(cdi_cl).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect data has been entered");
            }

            if (GetCurrentClaimValues.GetCurrentUserRole() == RoleNames.ROLE_ADMIN)
            {
                return RedirectToAction("RegisteredDevices", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Registrar");
            }
        }


        [HttpGet]
        public ActionResult DeleteDevice(int id)
        {

            var db = new MainDbContext();
            var neededDevice = db.clientDeviceInfo.Find(id);

            if (neededDevice == null)
            {
                return HttpNotFound();
            }

            db.clientDeviceInfo.Remove(neededDevice);
            db.SaveChanges();

            if (GetCurrentClaimValues.GetCurrentUserRole() == RoleNames.ROLE_ADMIN)
            {
                return RedirectToAction("RegisteredDevices", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Registrar");
            }
        }


        void SaveCategoriesAndSubcategories()
        {
            using (var db = new MainDbContext())
            {
                ViewBag.availableDeviceBins = db.deviceBin.ToList();
                ViewBag.availableDeviceConditions = db.deviceCondition.ToList();
                ViewBag.availableDeviceCategories = db.deviceCategory.ToList();
            }
        }
    }
}