using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace PlaylistManager.Services
{
    public class UsersService
    {
        private UnitOfWork unitOfWork;
        
        public UsersService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
                       
        public bool Create(User user)
        {            
            try
            {
                unitOfWork.UserRepository.Add(user);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(User user)
        {           
            try
            {
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
        public void Delete(User user)
        {
            unitOfWork.UserRepository.Delete(user);
            unitOfWork.Save();
        }

        public User GetById(int id)
        {
            return unitOfWork.UserRepository.GetById(id);
        }

        public List<User> GetAll()
        {
            return unitOfWork.UserRepository.GetAll();
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            return unitOfWork.UserRepository.Get(filter);
        }
    }
}
