using System.ComponentModel.DataAnnotations;
using Klicko_be.DTOs.OrderExperience;

namespace Klicko_be.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        public required List<ExperiencesListOrderExperienceDto>? OrderExperiences { get; set; }
    }
}
