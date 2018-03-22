using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlaylistManager.Services
{
    public static class AuthenticationManager
    {
        public static User LoggedUser
        {
            get
            {
                AuthenticationService authService;
                if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                {
                    HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();
                }
                authService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
                return authService.LoggedUser;
            }
        }

        public static void Authenticate(string username, string password)
        {
            AuthenticationService authService;
            if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
            {
                HttpContext.Current.Session["LoggedUser"] = new AuthenticationService();
            }
            authService = (AuthenticationService)HttpContext.Current.Session["LoggedUser"];
            authService.GetByUsernameAndPassword(username, password);
        }

        public static void Logout()
        {
            HttpContext.Current.Session["LoggedUser"] = null;
        }
    }
}
