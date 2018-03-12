using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using PlaylistManager.Services;
using PlaylistManager.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    [AdminFilter]
    [LoginFilter]
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            service = new UsersService(new UnitOfWork());
        }
        
        public ActionResult Index()
        {
            List<User> users = service.GetAll();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
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
            service.Delete(user);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}