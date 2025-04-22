using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class CreateCouponResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
