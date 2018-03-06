using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using PlaylistManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private UsersService service;

        public AccountController()
        {
            service = new UsersService(new UnitOfWork());
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (service.GetAll().Count == 0)
                {
                    user.IsAdmin = true;
                }
                service.Create(user);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                UsersService.LoggedUser = service.GetByEmailAndPassword(user.Username, user.Password);
                if (UsersService.LoggedUser != null)
                {
                    Session["Id"] = UsersService.LoggedUser.Id.ToString();
                    Session["Username"] = UsersService.LoggedUser.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                if (UsersService.LoggedUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Users");
                }
                if (!UsersService.LoggedUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Songs");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View();
        }
        
        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}