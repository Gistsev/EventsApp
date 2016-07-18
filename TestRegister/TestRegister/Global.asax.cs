﻿using TestRegister.Controllers;
using TestRegister.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TestRegister.App_Start;
using TestRegister.Business;
using WebMatrix.WebData;

namespace TestRegister
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            SimpleMembershipInitializer();
        }

        public void SimpleMembershipInitializer()
        {
            try {
                Repository.initDB();

                WebSecurity.InitializeDatabaseConnection("IdentityDb", "User", "id", "Email", autoCreateTables: true);
                if (!Roles.RoleExists("admin"))
                {
                    Roles.CreateRole("admin");
                }
                var admins = Roles.GetUsersInRole("admin");
                if (admins.Length == 0)
                {
                    try
                    {
                        var token = WebSecurity.CreateUserAndAccount("imagesharing.sigma@gmail.com", "111111",
                            new
                            {
                                imgURL = Tools.noPhoto
                            });
                        WebSecurity.ConfirmAccount(token);
                    }
                    catch
                    {

                    }
                    Roles.AddUserToRole("imagesharing.sigma@gmail.com", "admin");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }

    }
}
