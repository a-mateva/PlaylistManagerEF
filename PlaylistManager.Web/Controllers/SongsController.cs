using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.Controllers
{
    public class SongsController : Controller
    {
        // GET: Songs
        public ActionResult Index()
        {
            BaseRepository<Song> repo = new BaseRepository<Song>();
            List<Song> songs = repo.GetAll();
            return View(songs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Song item)
        {
            BaseRepository<Song> repo = new BaseRepository<Song>();
            if (ModelState.IsValid)
            {
                repo.Add(item);
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
            BaseRepository<Song> repo = new BaseRepository<Song>();
            Song item = repo.GetById(id);
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
            BaseRepository<Song> repo = new BaseRepository<Song>();
            repo.Delete(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            BaseRepository<Song> repo = new BaseRepository<Song>();
            Song item = repo.GetById(id);
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
            BaseRepository<Song> repo = new BaseRepository<Song>();

            if (ModelState.IsValid)
            {
                repo.Update(item);
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
    }
}