using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TestRegister.App_LocalResources;
using TestRegister.Business;
using TestRegister.DAL;
using TestRegister.Models;
using WebMatrix.WebData;

namespace TestRegister.Controllers
{
    public class AccountController : BaseController
    {

        protected UserHelper UserHelper = new UserHelper(SessionModel);

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register(ManageMessageId? message)
        {
            
            ViewBag.StatusMessage =
                 message == ManageMessageId.ConfirmYourEmail ? GlobalRes.ConfirmYourEmail
                 : message == ManageMessageId.Error ? GlobalRes.Error
                 : "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model, HttpPostedFileBase ImageFile)
        {
            string imgURL = Tools.noPhoto;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    
                    var token = WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                        new
                        {
                            imgURL = imgURL,
                            Name = model.Name,
                            Surname = model.Surname,
                            LastName = model.LastName,
                            PhoneNumber = model.PhoneNumber
                           
                        }, true);

                    var msg = string.Format(GlobalRes.Email_Confirm,
                         Url.Action("Confirm", "Account", new { token = token }, Request.Url.Scheme));

                    Tools.SendEmail(model.Email, "Email confirmation", msg);


                    if (ImageFile != null)
                    {
                        // сохранять картинку только в том случаее, если остальные данные правильные
                        imgURL = Tools.saveImg(ImageFile.InputStream, Server.MapPath("~/" + Tools.uploadPath));
                        var repo = new Repository();
                        var user = repo.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                        user.imgURL = imgURL;
                        repo.SaveChanges();
                    }

                }
                catch (MembershipCreateUserException e)
                {
                    return RedirectToAction("Register", new { Message = ManageMessageId.Error });
                }
                
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Register", new { Message = ManageMessageId.ConfirmYourEmail });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Confirm(string token)
        {
            var rez = WebSecurity.ConfirmAccount(token);
            return RedirectToAction("Manage", new { Message = rez ? ManageMessageId.EmailConfirm : ManageMessageId.EmailNotConfirm });
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? GlobalRes.PasswordHasBeenChanged
                : message == ManageMessageId.EmailConfirm ? GlobalRes.EmailWasConfirm
                : message == ManageMessageId.EmailNotConfirm ? GlobalRes.EmailWasNotConfirm
                : "";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.IsConfirmed(model.Email))
                {
                    ModelState.AddModelError("", GlobalRes.UserNotconfirmEmail);
                }
                else if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
                {
                    setupLoginData(model.Email);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", GlobalRes.EmailOrPasswordIncorrect);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }


        public ActionResult UserInformation()
        {
            var user = UserHelper.getUser();
            ViewBag.img = user.imgURL;
            ViewBag.FirstName = user.Name;
            ViewBag.LastName = user.LastName;
            ViewBag.Surname = user.Surname;
            ViewBag.Phone = user.PhoneNumber;
            return View();
        }

        public enum ManageMessageId
        {
            ConfirmYourEmail,
            ChangePasswordSuccess,
            EmailConfirm,
            EmailNotConfirm,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
