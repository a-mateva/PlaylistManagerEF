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
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            service = new UsersService(new UnitOfWork());
        }

        private bool IsAuthorized()
        {
            if (Session["LoggedUser"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult Index()
        {
            if (!IsAuthorized() || IsAdmin())
            {
                return RedirectToAction("Login", "Account");
            }
            List<User> users = service.GetAll();
            return View(users);
        }

        public ActionResult Create()
        {
            if (!IsAuthorized() || !IsAdmin())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                service.Create(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Update(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Account");
            }
            User user = service.GetById(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                service.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Account");
            }
            User user = service.GetById(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Delete(User user)
        {
            if (!IsAuthorized() || !IsAdmin())
            {
                return RedirectToAction("Login", "Account");
            }
            service.Delete(user);
            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            if (UsersService.LoggedUser.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}