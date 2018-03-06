using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistManager.Web.ViewModels
{
    public class PlaylistViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Song> Songs { get; set; }
        public List<SelectListItem> SelectedSongs { get; set; }
    }
}