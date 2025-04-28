using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.FidelityCard
{
    public class ConvertPointsRequestDto
    {
        [Required]
        public required int Points { get; set; }
    }
}
