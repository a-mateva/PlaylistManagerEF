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
    public class SongsController : Controller
    {
        private SongsService service;

        public SongsController()
        {
            service = new SongsService(new UnitOfWork());
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
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            List<Song> songs = service.GetAll();
            return View(songs);
        }

        public ActionResult Create()
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Song item)
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            if (service.Create(item))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            Song item = service.GetById(id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Delete(Song item)
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            service.Delete(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            Song item = service.GetById(id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Update(Song item)
        {
            if (!IsAuthorized())
            {
                RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                service.Update(item);
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}