using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Order
{
    public class EditOrderStateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
