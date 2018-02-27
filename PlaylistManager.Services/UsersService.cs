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
        private ModelStateDictionary modelState;

        public UsersService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetByEmailAndPassword(string username, string password)
        {
            User LoggedUser = unitOfWork.UserRepository.Get(u => u.Username == username && u.Password == password).FirstOrDefault();
            return LoggedUser;
        }

        public bool Validate(User user)
        {
            if (user.Name.Trim().Length == 0)
            {
                modelState.AddModelError("Name", "Name is required");
            }
            if (user.Username.Trim().Length == 0)
            {
                modelState.AddModelError("Username", "Username is required");
            }
            if (user.Password.Trim().Length == 0)
            {
                modelState.AddModelError("Password", "You must choose a password longer than 7 characters.");
            }
            if (user.Password.Trim().Length < 7)
            {
                modelState.AddModelError("Password", "Password is too short.");
            }
            return modelState.IsValid;
        }

        public bool Create(User user)
        {
            if (!Validate(user))
            {
                return false;
            }
            try
            {
                unitOfWork.UserRepository.Add(user);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(User user)
        {
            if (!Validate(user))
            {
                return false;
            }
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
