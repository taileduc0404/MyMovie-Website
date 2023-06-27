using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MVC_Movie_Tai.Models
{
    public class Movie
    {

        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }
        //public string? Image { get; set; }
        public string? Genre { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Price { get; set; }
    }
}
