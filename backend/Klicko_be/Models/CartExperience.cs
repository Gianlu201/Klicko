using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klicko_be.Models
{
    public class CartExperience
    {
        [Key]
        public Guid CartExperienceId { get; set; }

        public required Guid ExperienceId { get; set; }
        public required Guid CartId { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigazione
        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }

        [ForeignKey(nameof(ExperienceId))]
        public Experience? Experience { get; set; }
    }
}
