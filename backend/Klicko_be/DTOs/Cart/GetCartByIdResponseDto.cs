using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Cart
{
    public class GetCartByIdResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required CartDto? Cart { get; set; }
    }
}
