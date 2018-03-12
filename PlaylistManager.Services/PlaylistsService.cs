using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Services
{
    public class PlaylistsService
    {
        private UnitOfWork unitOfWork;

        public PlaylistsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool Create(Playlist playlist)
        {
            try
            {
                unitOfWork.PlaylistsRepository.Add(playlist);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Playlist playlist)
        {
            try
            {
                unitOfWork.PlaylistsRepository.Update(playlist);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Delete(Playlist playlist)
        {
            unitOfWork.PlaylistsRepository.Delete(playlist);
            unitOfWork.Save();
        }

        public List<Playlist> GetAll(Expression<Func<Playlist, bool>> filter)
        {
            return unitOfWork.PlaylistsRepository.GetAll(filter);
        }
        
        public Playlist GetById(int id)
        {
            return unitOfWork.PlaylistsRepository.GetById(id);
        }
    }
}
