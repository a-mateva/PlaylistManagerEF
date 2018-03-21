using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Services
{
    public class AuthenticationService
    {
        private UnitOfWork unitOfWork;
        public User LoggedUser { get; set; }
        
        public User GetByUsernameAndPassword(string username, string password)
        {
            unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.Get(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                LoggedUser = user;
            }
            return LoggedUser;
        }
    }
}
