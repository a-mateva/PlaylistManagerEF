using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public class Song : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        public string YearReleased { get; set; }

        public virtual List<Playlist> Playlists { get; set; }
    }
}
