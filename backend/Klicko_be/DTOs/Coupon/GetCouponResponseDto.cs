using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class GetCouponResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required CouponDto? Coupon { get; set; }
    }
}
