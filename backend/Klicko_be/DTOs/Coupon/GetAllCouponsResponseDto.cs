using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class GetAllCouponsResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<CouponDto>? AvailableCoupons { get; set; }

        [Required]
        public required List<CouponDto>? UnavailableCoupons { get; set; }
    }
}
