using System.ComponentModel.DataAnnotations;
using Klicko_be.DTOs.OrderExperience;

namespace Klicko_be.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        public required List<ExperiencesListOrderExperienceDto>? OrderExperiences { get; set; }

        [Required]
        public int? PercentualSaleAmount { get; set; }

        [Required]
        public int? FixedSaleAmount { get; set; }
    }
}
