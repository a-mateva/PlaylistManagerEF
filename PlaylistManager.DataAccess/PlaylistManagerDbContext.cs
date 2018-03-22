using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.DataAccess
{
    public class PlaylistManagerDbContext : DbContext
    {
        public PlaylistManagerDbContext()
            : base("PlaylistManagerDb")
        {

        }

        DbSet<User> Users { get; set; }
        DbSet<Playlist> Playlists { get; set; }
        DbSet<Song> Songs { get; set; }
        DbSet<CustomException> CustomExceptions { get; set; }
    }
}
