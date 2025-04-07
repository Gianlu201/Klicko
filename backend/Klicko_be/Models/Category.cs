using System.ComponentModel.DataAnnotations;

namespace Klicko_be.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public string? Image { get; set; }

        public string? Icon { get; set; }

        // navigazione
        public ICollection<Experience>? Experiences { get; set; }
    }
}
