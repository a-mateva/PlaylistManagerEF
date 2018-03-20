using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private PlaylistManagerDbContext context = new PlaylistManagerDbContext();
        private BaseRepository<User> usersRepo;
        private BaseRepository<Playlist> playlistsRepo;
        private BaseRepository<Song> songsRepo;
        private BaseRepository<CustomException> exceptionsRepo;

        private bool disposed;

        public BaseRepository<User> UserRepository
        {
            get
            {
                if (usersRepo == null)
                {
                    usersRepo = new BaseRepository<User>(context);
                }
                return usersRepo;
            }
        }

        public BaseRepository<Song> SongsRepository
        {
            get
            {
                if (songsRepo == null)
                {
                    songsRepo = new BaseRepository<Song>(context);
                }
                return songsRepo;
            }
        }

        public BaseRepository<Playlist> PlaylistsRepository
        {
            get
            {
                if (playlistsRepo == null)
                {
                    playlistsRepo = new BaseRepository<Playlist>(context);
                }
                return playlistsRepo;
            }
        }


        public BaseRepository<CustomException> CustomExceptionsRepository
        {
            get
            {
                if (exceptionsRepo == null)
                {
                    exceptionsRepo = new BaseRepository<CustomException>(context);
                }
                return exceptionsRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
