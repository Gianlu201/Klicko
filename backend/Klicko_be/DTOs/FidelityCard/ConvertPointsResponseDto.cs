using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.FidelityCard
{
    public class ConvertPointsResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
