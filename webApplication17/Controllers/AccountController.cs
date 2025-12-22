using AlifSani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AlifSani.Models.EntityFramework;

namespace AlifSani.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = new Entities().Users
               .Any(u => u.UserName.ToLower() == user.UserName.ToLower() && user.Password == user.Password);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Dashboard");

                }

            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult ChangePassword()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            int response = -1;

            try
            {
                using (Entities db = new Entities())
                {
                    var User = db.Users.Where(x => x.Password == changePassword.OldPassword && x.UserName.ToLower() == System.Web.HttpContext.Current.User.Identity.Name.ToLower()).FirstOrDefault();
                    if (User != null)
                    {
                        User.Password = changePassword.NewPassword;
                        db.SaveChanges();
                        response = 1;

                    }

                }

            }
            catch (Exception ex)
            {
                response = -1;
            }
            return Json(new { Code = response }, JsonRequestBehavior.AllowGet);
        }

    }
}