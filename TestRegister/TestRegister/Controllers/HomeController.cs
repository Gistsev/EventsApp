using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TestRegister.App_LocalResources;
using TestRegister.Business;
using TestRegister.DAL;
using TestRegister.DAL.Entity;
using TestRegister.Models;

namespace TestRegister.Controllers
{
    public class HomeController : BaseController
    {
        protected UserHelper UserHelper = new UserHelper(SessionModel);
        EventHelper helper = new EventHelper(new Repository());
        CommentHelper comm = new CommentHelper(new Repository());
        GroupEventHelper helperGroups = new GroupEventHelper(new Repository());
        [AllowAnonymous]
        public ActionResult ChangeLang(string lang, string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                if (UserHelper.changeLang(lang))
                {
                    Session["Lang"] = lang;
                    Tools.setUpLocale(lang);
                }
            }
            else
            {
                try
                {
                    Session["Lang"] = lang;
                    Tools.setUpLocale(lang);
                }
                catch
                {
                    Session["Lang"] = "en";
                    Tools.setUpLocale("en");
                }
            }
            return Redirect(returnUrl);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "My contact page.";

            return View();
        }


        public ActionResult EventsView()
        {
            return View(helperGroups.GetGroups());
        }

        public ActionResult SpecificGroup(int id)
        {
            var vm = new EventViewModel() { Events = helper.GetEvents(id), GroupEvent = helperGroups.GetGroup(id) };
            return View(vm);
        }

        public ActionResult SpecificEvent(int id)
        {
            var vt = new SpecificViewModel() {Comments = comm.GetComments(id), Event = helper.GetEvent(id)};
            return View(vt);
        }

        public ActionResult Create()
        {
            return View(new Comment());
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Comment s, int id)
        {
            try
            {
               
                comm.AddComment(s.Message, id);
                return RedirectToAction("SpecificEvent", new {id});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(comm.GetComment(id));
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                comm.DeleteComment(id);
                return RedirectToAction("SpecificEvent", "Home", new {id});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(Event ev, int id)
        {
            try
            {

                helper.AddEvent(ev.Title, ev.Description, id);
                return RedirectToAction("SpecificGroup", new { id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteEvent(int id)
        {
            return View(new Event());
        }

        [HttpPost]
        public ActionResult DeleteEvent(int id, FormCollection collection)
        {
            try
            {
                helper.DeleteEvent(id);
                return View("SpecificGroup");
            }
            catch
            {
                return View();
            }
        }
    }
}