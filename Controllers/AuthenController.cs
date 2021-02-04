using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TrainingMVCProject.Models;

namespace TrainingMVCProject.Controllers
{
    public class AuthenController : Controller
    {
        TrainingDBEntities1 db = new TrainingDBEntities1();
        // GET: Authen
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            // check username exist
            //todo later
            //2 check password = confirmPassword
            if (ModelState.IsValid)
            {
                if (!account.Password.Equals(account.ConfirmPassword))
                {
                    return View(account);
                }
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);

                var user = new IdentityUser() { UserName = account.UserName };

                IdentityResult result = userManager.Create(user, account.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("error", "cannot create user!");
                    return View(account);
                }
                else
                {
                    
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(account);
            }
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Account acc)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = manager.Find(acc.UserName, acc.Password);

            //manager.ChangePassword(acc.UserName, acc.Password);
            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties { }, userIdentity);
                return RedirectToAction("index", "Home");
            }else
            {
                ModelState.AddModelError("", "Invalid username or password ");
            }    
            return View(acc);
        }
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("login", "Authen");

        }
        public static void CreateAccount(string userName, string password, string role)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            //create user with username, password
            var user = new IdentityUser(userName);
            IdentityResult result = manager.Create(user, password);

            //add user to role
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, role);
            }



        }
    }
}

