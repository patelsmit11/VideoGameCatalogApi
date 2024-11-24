using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameCatalogueApi.Models
{
    /// <summary>
    /// Class representing a Video Game entity in the database
    /// </summary>
    public class VideoGame
    {
        // Primary Key for the VideoGame entity
        public int Id { get; set; }

        // The title of the video game (Required field)
        [Required]
        public required string Title { get; set; }

        // Genre of the video game (Required field)
        [Required]
        public int Genre { get; set; }

        // The rating of the video game (Required field). Must be a value between 0 and 5
        [Required]
        [Range(0, 5)]
        public decimal Rating { get; set; }

        // The price of the video game (Required field). Must be a positive value (greater than 0)
        [Required]
        [Precision(18, 2)]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        // The publisher of the video game (Optional field)
        public string? Publisher { get; set; }

        // Release date of the video game (Required field)
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ReleaseDate { get; set; }
    }
}
