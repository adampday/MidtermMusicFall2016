using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Models
{
    public class Playlist
    {
        // Primary key
        public int playListID { get; set; }
        public string name { get; set; }
        public string owner { get; set; }

        // Foreign key
        public int UserID { get; set; }
        public ApplicationUser user { get; set; } 
        // a list of albums in a playlist
        public List<PlaylistExtension> list { get; set; }
    }
}
