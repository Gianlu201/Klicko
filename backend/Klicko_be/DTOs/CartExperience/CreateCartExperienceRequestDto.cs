using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.CartExperience
{
    public class CreateCartExperienceRequestDto
    {
        [Required]
        public required Guid ExperienceId { get; set; }
    }
}
