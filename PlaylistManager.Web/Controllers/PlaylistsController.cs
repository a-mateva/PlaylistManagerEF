using PlaylistManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using PlaylistManager.Web.ViewModels;

namespace PlaylistManager.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private PlaylistsService pService;
        private SongsService sService;
        private UnitOfWork unitOfWork;

        public PlaylistsController()
        {
            unitOfWork = new UnitOfWork();
            pService = new PlaylistsService(unitOfWork);
            sService = new SongsService(unitOfWork);
        }

        private bool IsAuthenticated()
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

        [HttpGet]
        public ActionResult Create()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Song> allSongs = sService.GetAll();
                PlaylistViewModel model = new PlaylistViewModel();
                model.SelectedSongs = new List<SelectListItem>();
                model.UserId = UsersService.LoggedUser.Id;

                foreach (var item in allSongs)
                {
                    model.SelectedSongs.Add(new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(PlaylistViewModel model, string[] selectedSongs)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Playlist playlist = new Playlist();

                playlist.Name = model.Name;
                playlist.Description = model.Description;
                playlist.UserId = UsersService.LoggedUser.Id;

                pService.Create(playlist);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Playlist> playlists = pService.GetAll(list => list.UserId == UsersService.LoggedUser.Id);
                return View(playlists);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Playlist playlist = pService.GetById(id);

                PlaylistViewModel model = new PlaylistViewModel();
                model.Name = playlist.Name;
                model.Description = playlist.Description;
                model.UserId = playlist.UserId;

                List<Song> allSongs = sService.GetAll();

                model.SelectedSongs = new List<SelectListItem>();

                int count = -1;
                foreach (var item in allSongs)
                {
                    count++;
                    model.SelectedSongs.Add(new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
                    foreach (var song in playlist.Songs)
                    {
                        if (song.Id == item.Id)
                        {
                            model.SelectedSongs[count].Selected = true;
                            break;
                        }
                    }
                }

                if (playlist == null)
                {
                    return HttpNotFound();
                }

                return View(model);
            }
        }

        private List<Song> GetSelectedSongs(string[] selectedSongs)
        {
            List<Song> result = new List<Song>();
            foreach (string item in selectedSongs)
            {
                result.Add(sService.GetById(Convert.ToInt32(item)));
            }
            return result;
        }

        [HttpPost]
        public ActionResult Edit(Playlist model, string[] selectedSongs)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Playlist playlist = pService.GetById(model.Id);
                playlist.Name = model.Name;
                playlist.UserId = UsersService.LoggedUser.Id;
                playlist.Description = model.Description;
                playlist.Songs.Clear();

                try
                {
                    playlist.Songs = GetSelectedSongs(selectedSongs);
                    pService.Update(playlist);
                }

                catch (Exception)
                {
                    return RedirectToAction("Edit");
                }

                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Delete(int id)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Playlist playlist = pService.GetById(id);
                if (playlist == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(playlist);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(Playlist playlist)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                pService.Delete(playlist);
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}