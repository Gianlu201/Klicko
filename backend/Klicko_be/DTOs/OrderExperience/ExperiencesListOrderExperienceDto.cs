using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.OrderExperience
{
    public class ExperiencesListOrderExperienceDto
    {
        [Required]
        public required Guid ExperienceId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
