using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Experience;

namespace Klicko_be.DTOs.Order
{
    public class GetOrderByIdResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required OrderDto? Order { get; set; }
    }
}
