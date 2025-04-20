using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Cart
{
    public class EditCartResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
