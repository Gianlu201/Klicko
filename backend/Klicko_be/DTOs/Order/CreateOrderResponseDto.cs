using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Order
{
    public class CreateOrderResponseDto
    {
        [Required]
        public required string Message { get; set; }

        public Guid OrderId { get; set; }
    }
}
