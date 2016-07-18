using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestRegister.Business;
using TestRegister.DAL;
using TestRegister.DAL.Entity;
using WebMatrix.WebData;

namespace TestRegister.Controllers
{
    public abstract class BaseController : Controller
    {
        protected static SessionModelSingleton SessionModel = SessionModelSingleton.MyInstance;
        IRepository repo = new Repository();


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Debug.WriteLine("Initialize");
            base.Initialize(requestContext);
            SessionModel.repo = repo;
            SessionModel.serverBasePath = AppDomain.CurrentDomain.GetData("APPBASE").ToString();

            Debug.WriteLine("WebSecurity.IsAuthenticated:" + WebSecurity.IsAuthenticated);
            if (WebSecurity.IsAuthenticated)
            {
                if (Session["UserId"] == null)
                    setupLoginData(WebSecurity.CurrentUserName);

                SessionModel.userId = (int)Session["UserId"];
                SessionModel.isAdmin = Roles.Provider.IsUserInRole(WebSecurity.CurrentUserName, "admin");
            }
            Session["Lang"] = Session["Lang"] ?? "en";
            Tools.setUpLocale(Session["Lang"] as string);
        }

        protected void setupLoginData(string email)
        {
            var repo = new Repository();
            var user = repo.Users.First(u => u.Email.ToUpper() == email.ToUpper());

            Session["UserId"] = user.Id;
            Session["UserName"] = Tools.getName(user);
            Session["Lang"] = user.lang;
        }

       
    }
}