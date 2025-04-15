using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klicko_be.Models
{
    public class Image
    {
        [Key]
        public Guid ImageId { get; set; }

        [Required]
        public required string Url { get; set; }

        public required Guid ExperienceId { get; set; }

        // navigazione
        [ForeignKey(nameof(ExperienceId))]
        public Experience? Experience { get; set; }
    }
}
