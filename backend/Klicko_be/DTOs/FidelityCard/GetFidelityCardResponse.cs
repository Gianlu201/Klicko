using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.FidelityCard
{
    public class GetFidelityCardResponse
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required FidelityCardDto? FidelityCard { get; set; }
    }
}
