using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace PlaylistManager.Services
{
    public class SongsService
    {
        private UnitOfWork unitOfWork;

        public SongsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        
        public Song GetById(int id)
        {
            return unitOfWork.SongsRepository.GetById(id);
        }

        public List<Song> GetAll()
        {
            return unitOfWork.SongsRepository.GetAll();
        }

        public bool Create(Song song)
        {
            try
            {
                unitOfWork.SongsRepository.Add(song);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Delete(Song song)
        {
            unitOfWork.SongsRepository.Delete(song);
            unitOfWork.Save();
        }

        public bool Update(Song song)
        {            
            try
            {
                unitOfWork.SongsRepository.Update(song);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
