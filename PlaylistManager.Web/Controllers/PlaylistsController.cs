using PlaylistManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlaylistManager.DataAccess;

namespace PlaylistManager.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private PlaylistsService service;

        public PlaylistsController()
        {
            service = new PlaylistsService(new UnitOfWork());
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}