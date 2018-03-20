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
    [AllowAnonymous]
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

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            //if (ModelState.IsValid)
            //{
                if (service.GetAll().Count == 0)
                {
                    user.IsAdmin = true;
                }
                service.Create(user);
                return RedirectToAction("Login");
            //}
            //else
            //{
                //return RedirectToAction("Register");
            //}
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                AuthenticationManager.Authenticate(user.Username, user.Password);
                if (AuthenticationManager.LoggedUser == null)
                {
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Index", "Playlists");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View();
        }        
    }
}