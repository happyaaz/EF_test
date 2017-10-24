using LostAndFound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using LostAndFound.CustomLibraries;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LostAndFound.Helpers;

namespace LostAndFound.Controllers
{

    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        //  will be triggered by the "submit" button from the view
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(Models.UserLoginInfo uli_cl)
        {
            //  check if there are any errors inside the form's values
            if (!ModelState.IsValid)
            {
                return View(uli_cl);
            }

            using (var dbLogin = new MainDbContext())
            {

                var userLoginInfo = dbLogin.applicationUser.FirstOrDefault(u => u.UserName == uli_cl.username);

                if (userLoginInfo != null)
                {
                    var getPassword = dbLogin.applicationUser.Where(u => u.UserName == uli_cl.username).Select(u => u.Password);
                    var materializePassword = getPassword.ToList();
                    var password = materializePassword[0];
                    var decryptedPassword = CustomDecrypt.Decrypt(password);

                    if (uli_cl.password == decryptedPassword && userLoginInfo.UserName == uli_cl.username)
                    {
                        var getUserFullName = dbLogin.applicationUser.Where(u => u.UserName == uli_cl.username).Select(u => u.UserFullName);
                        var materializeFullName = getUserFullName.ToList();
                        var userFullName = materializeFullName[0];

                        //  we need to store the employer somewhere
                        var getUserEmployer = dbLogin.applicationUser.Where(u => u.UserName == uli_cl.username).Select(u => u.UserEmployer);
                        var materializeUserEmployer = getUserEmployer.ToList();
                        var userEmployer = materializeUserEmployer[0];


                        var getUserRole = dbLogin.applicationUser.Where(u => u.UserName == uli_cl.username).Select(u => u.UserRole);
                        var materializeUserRole = getUserRole.ToList();
                        var userRole = materializeUserRole[0];


                        var getUserId = dbLogin.applicationUser.Where(u => u.UserName == uli_cl.username).Select(u => u.Id);
                        var materializeUserId = getUserId.ToList();
                        var userId = materializeUserId[0].ToString();


                        //  create an identity with name and role
                        var identity = new ClaimsIdentity(new[] {
                                new Claim (ClaimTypes.Name, userFullName),
                                new Claim (ClaimTypes.Role, userRole),
                                new Claim ("userId", userId),
                                new Claim ("clientName", userEmployer),
                            }, "ApplicationCookie");

                        //  actually login with identity
                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;
                        authManager.SignIn(identity);

                        if (userRole == RoleNames.ROLE_ADMIN)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (userRole == RoleNames.ROLE_REGISTRAR)
                        {
                            return RedirectToAction("Index", "Registrar");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Client");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password");
                        return View(uli_cl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(uli_cl);
                }
            }
        }


        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }



        [Authorize(Roles = RoleNames.ROLE_ADMIN)]
        [HttpGet]
        public ActionResult Registration()
        {

            //  get all the possible roles
            //List<string> roles_list = new List<string>();
            using (var db = new MainDbContext())
            {
                //roles_list = db.roles_list.Select (x=>x.roleName).ToList();
                var availableRows = db.roles.ToList();
                availableRows.RemoveAt(2);
                ViewBag.availableRolesNames = availableRows;
            }

            //ViewBag.availableRolesNames = roles_list;

            //  get all the possible clients
            using (var db = new MainDbContext())
            {
                ViewBag.availableClientsNames = db.clientBasicInfo.ToList();
            }



            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        //[Authorize(Roles = RoleNames.ROLE_ADMIN)]
        public ActionResult Registration(UserFullView ufv_cl)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    //Console.WriteLine(ufv_cl);
                    string encryptedPassword_str = CustomEncrypt.Encrypt(ufv_cl.uli_cl.password);

                    var applicationUser = db.applicationUser.Create();


                    applicationUser.Password = encryptedPassword_str;
                    applicationUser.UserName = ufv_cl.uli_cl.username;
                    applicationUser.UserFullName = ufv_cl.ubi_cl.userFullName;
                    applicationUser.UserEmployer = ufv_cl.ubi_cl.clientBasicInfo.clientName;
                    applicationUser.UserRole = ufv_cl.ubi_cl.lostAndFoundRoles.roleName;

                    db.applicationUser.Add(applicationUser);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        [Authorize(Roles = RoleNames.ROLE_ADMIN)]
        public ActionResult RegisterClient()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = RoleNames.ROLE_ADMIN)]
        public ActionResult RegisterClient(ClientBasicInfo cbi)
        {
            using (var db = new MainDbContext())
            {
                var client = db.clientBasicInfo.Create();
                client = cbi;
                db.clientBasicInfo.Add(client);
                db.SaveChanges();
            }
            return RedirectToAction("RegisteredClients", "Admin");
        }
    }
}