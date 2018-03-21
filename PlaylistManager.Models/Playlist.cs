using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public class Playlist : BaseModel
    {
        public Playlist()
        {

        }

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }

        public virtual List<Song> Songs { get; set; }
        public virtual User User { get; set; }
    }
}
