using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class EditCouponResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
