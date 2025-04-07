using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klicko_be.Models
{
    public class CarryWith
    {
        [Key]
        public Guid CarryWithId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required Guid ExperienceId { get; set; }

        // navigazione
        [ForeignKey(nameof(ExperienceId))]
        public Experience? Experience { get; set; }
    }
}
