using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Services
{
    public class CustomExceptionsService
    {
        private UnitOfWork unitOfWork;

        public CustomExceptionsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public CustomException GetById(int id)
        {
            return unitOfWork.CustomExceptionsRepository.GetById(id);
        }

        public List<CustomException> GetAll()
        {
            return unitOfWork.CustomExceptionsRepository.GetAll();
        }
    }
}
