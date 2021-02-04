﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(TrainingMVCProject.Startup))]

namespace TrainingMVCProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Authen/Login")
            });
            CreateUserRoles();
        }
        private void CreateUserRoles()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            var user = new IdentityUser("admin");
            var result = manager.Create(user, "admin1");

            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, "Admin");
            }


            if (!roleManager.RoleExists("TrainingStaff"))
            {
                var role = new IdentityRole("TrainingStaff");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Trainer"))
            {
                var role = new IdentityRole("Trainer");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Trainee"))
            {
                var role = new IdentityRole("Trainee");
                roleManager.Create(role);
            }

        }
    }
}
