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
    public class UsersService
    {
        private UnitOfWork unitOfWork;
        public static User LoggedUser { get; set; }
        
        public UsersService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetByEmailAndPassword(string username, string password)
        {
            LoggedUser = unitOfWork.UserRepository.Get(u => u.Username == username && u.Password == password).FirstOrDefault();
            return LoggedUser;
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
    }
}
