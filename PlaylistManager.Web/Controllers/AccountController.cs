﻿using PlaylistManager.DataAccess;
using PlaylistManager.Logger;
using PlaylistManager.Models;
using PlaylistManager.Services;
using PlaylistManager.Web.ViewModels;
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

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                User getUser = service.Get(u => user.Email == u.Email);
                if (getUser.Email != null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    if (service.GetAll().Count == 0)
                    {
                        user.IsAdmin = true;
                    }
                    service.Create(user);
                    emailService = new EmailSendingService();
                    await emailService.SendConfirmationEmailAsync(user);
                    return RedirectToAction("Login");

                }
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
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                User currentUser = service.Get(u => u.Username == model.Username && u.Password == model.Password);
                if (currentUser != null)
                {
                    AuthenticationManager.Authenticate(currentUser.Username, currentUser.Password);
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