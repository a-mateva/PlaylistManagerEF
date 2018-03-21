using PlaylistManager.DataAccess;
using PlaylistManager.Logger;
using PlaylistManager.Models;
using PlaylistManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ILog logger = Logger.Logger.GetInstance;
        private UsersService service;
        private EmailSendingService emailService;

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
        public async Task<ActionResult> Register(User user)
        {
            //if (!user.Email.Contains("@gmail.com"))
            //{
            //    ViewData["RegisterUnsuccessful"] = "Only gmail accounts are accepted.";
            //    return View(user);
            //}
            try
            {                
                if (service.GetById(user.Id) != null)
                {
                    ViewData["RegisterUnsuccessful"] = "This email is already taken.";
                    return View(user);
                }
                if (service.GetAll().Count == 0)
                {
                    user.IsAdmin = true;
                }
                service.Create(user);
                emailService = new EmailSendingService();
                await emailService.SendConfirmationEmailAsync(user);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                logger.LogCustomException(ex, null);
                return RedirectToAction("Error", "Home");
            }            
        }

        [HttpGet]
        public ActionResult ValidateEmail(int id)
        {
            try
            {
                User user = service.GetById(id);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                service.Update(user);
            }
            catch (Exception ex)
            {
                logger.LogCustomException(ex, null);
                return RedirectToAction("Index", "Home");
            }
            return View("ConfirmEmail"); //TODO
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                AuthenticationManager.Authenticate(user.Username, user.Password);
                if (!user.IsEmailConfirmed)
                {
                    ViewData["LoginUnsuccessful"] = "Email is not confirmed!";
                    return View();
                }
                if (AuthenticationManager.LoggedUser == null)
                {
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", "Invalid email or password.");
                logger.LogCustomException(ex, null);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}