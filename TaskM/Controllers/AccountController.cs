﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models.Entity_Models;
using Models.VM.AccountViewModel;

namespace TaskM.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Login()
        {
            LoginViewModel model= new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationSignInManager signInManager = Request.GetOwinContext().Get<ApplicationSignInManager>();
            SignInStatus signInStatus = signInManager.PasswordSignIn(model.Email, model.Password, false, false);
            switch (signInStatus)
            {
                case SignInStatus.Success:
                    return Redirect("/");
                    break;
                default: ModelState.AddModelError("","Invalid login attempt");
                    return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUserManager userManager = Request.GetOwinContext().Get<ApplicationUserManager>();
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                PasswordHash = new PasswordHasher().HashPassword(model.Password),
                UserName = model.Email
            };
            userManager.Create(user);

            IdentityResult result = userManager.Create(user);
            if (result.Succeeded)
            {
                ApplicationSignInManager signInManager = Request.GetOwinContext().Get<ApplicationSignInManager>();
                signInManager.SignIn(user, false, false);
               return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}