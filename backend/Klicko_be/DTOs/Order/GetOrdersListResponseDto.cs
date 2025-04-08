using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Order
{
    public class GetOrdersListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<OrderDto>? Orders { get; set; }
    }
}
