using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using ViewModels;

namespace systemMangement.Controllers
{
    public class AccountController : Controller
    {
        private SystemManagementDBEntities db = new SystemManagementDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Users.FirstOrDefault(c => c.UserName == login.UserName && c.Password==login.Password);
                if (user != null)
                {
                    Session["usserInfo"] = user.UserName;
                    Session["usserId"] = user.ID;
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return RedirectToAction("Index", "Tasks");
                }
                else
                    ModelState.AddModelError("UserName", "کاربری یافت نشد");
            }
            return View(login);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}