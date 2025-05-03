using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.CartExperience
{
    public class CreateCartExperienceFromLocalCartRequestDto
    {
        [Required]
        public Guid ExperienceId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
