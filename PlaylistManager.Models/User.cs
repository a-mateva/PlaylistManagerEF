using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public class User : BaseModel
    {
        public User()
        {

        }

        [Required]
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [Required]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password is too short.")]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public virtual List<Playlist> Playlists { get; set; }
    }
}
