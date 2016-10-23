using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        [Required(ErrorMessage = "Please enter in a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter in a bio.")]
        public string Bio { get; set; }


    }
}