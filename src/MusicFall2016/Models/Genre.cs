using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Please enter in a name.")]
        [MaxLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
    }
}