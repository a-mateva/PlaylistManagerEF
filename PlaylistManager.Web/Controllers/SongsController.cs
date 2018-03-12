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
    public class SongsController : Controller
    {
        private SongsService service;

        public SongsController()
        {
            service = new SongsService(new UnitOfWork());
        }
                
        public ActionResult Index()
        {
            List<Song> songs = service.GetAll();
            return View(songs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Song item)
        {
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
            service.Delete(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
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