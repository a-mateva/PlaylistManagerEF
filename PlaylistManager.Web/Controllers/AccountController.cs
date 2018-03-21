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

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {                
                if (service.GetById(user.Id) != null)
                {
                    ModelState.AddModelError("email", "Email address is already taken.");
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
        [AllowAnonymous]
        public ActionResult ValidateEmail(int userId)
        {
            try
            {
                User user = service.GetById(userId);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    user.IsEmailConfirmed = true;
                }

                service.Update(user);
            }
            catch (Exception ex)
            {
                logger.LogCustomException(ex, null);
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {            
            try
            {
                AuthenticationManager.Authenticate(user.Username, user.Password);
                if (AuthenticationManager.LoggedUser != null)
                {
                    if (!AuthenticationManager.LoggedUser.IsEmailConfirmed)
                    {
                        return View(user);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                logger.LogCustomException(ex, null);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}