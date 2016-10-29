using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Please enter in a name.")]
        [StringLength(20, ErrorMessage = "Name cannot be more than 20 letters!")]
        public string Name { get; set; }
    }
}