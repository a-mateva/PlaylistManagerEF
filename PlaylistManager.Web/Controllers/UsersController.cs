using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            BaseRepository<User> repo = new BaseRepository<User>();
            List<User> users = repo.GetAll();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            BaseRepository<User> repo = new BaseRepository<User>();
            if (ModelState.IsValid)
            {
                repo.Add(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Update(int id)
        {
            BaseRepository<User> repo = new BaseRepository<User>();
            User user = repo.GetById(id);
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
            BaseRepository<User> repo = new BaseRepository<User>();
            if (ModelState.IsValid)
            {
                repo.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Delete(int id)
        {
            BaseRepository<User> repo = new BaseRepository<User>();
            User user = repo.GetById(id);
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
            BaseRepository<User> repo = new BaseRepository<Models.User>();
            repo.Delete(user);
            return RedirectToAction("Index");
        }
    }
}