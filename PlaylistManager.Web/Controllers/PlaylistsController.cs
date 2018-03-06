﻿using PlaylistManager.Services;
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
    [Authorize]
    public class PlaylistsController : Controller
    {
        private PlaylistsService pService;
        private SongsService sService;
        private UnitOfWork unitOfWork;

        public PlaylistsController()
        {
            pService = new PlaylistsService(unitOfWork);
            sService = new SongsService(unitOfWork);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            List<Song> allSongs = sService.GetAll();
            PlaylistViewModel model = new PlaylistViewModel();
            model.SelectedSongs = new List<SelectListItem>();

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

        [HttpPost]
        public ActionResult Create(PlaylistViewModel model, string[] selectedSongs)
        {
            Playlist playlist = new Playlist();
            playlist.Name = model.Name;
            playlist.Songs = GetSelectedSongs(selectedSongs);
            if (ModelState.IsValid)
            {
                pService.Create(playlist);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }        

        public ActionResult Index()
        {
            List<Playlist> playlists = pService.GetAll(list => list.UserId == UsersService.LoggedUser.Id);
            return View(playlists);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PlaylistViewModel model = new PlaylistViewModel();
            Playlist playlist = pService.GetById(id);

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
        public ActionResult Edit(Playlist playlistToEdit, string[] selectedSongs)
        {
            PlaylistViewModel model = new PlaylistViewModel();
            Playlist playlist = playlistToEdit;
            playlist.Songs.Clear();

            try
            {
                playlist.Songs = GetSelectedSongs(selectedSongs);
            }
            catch (Exception)
            {
                return RedirectToAction("Edit");
            }
            pService.Update(playlist);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
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

        [HttpPost]
        public ActionResult Delete(Playlist playlist)
        {
            pService.Delete(playlist);
            return RedirectToAction("Index");
        }
    }
}