using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Models
{
    public class PlaylistExtension
    {
        public int playlistID { get; set; }
        public int albumID { get; set; }
        public Playlist playlist { get; set; }
        public Album album { get; set; }
    }
}
