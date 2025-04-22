using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class CouponDto
    {
        [Required]
        public Guid CouponId { get; set; }

        public int PercentualSaleAmount { get; set; } = 0;

        public int FixedSaleAmount { get; set; } = 0;

        [Required]
        public required bool IsActive { get; set; }

        public required bool IsUniversal { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        public required int MinimumAmount { get; set; }

        [Required]
        public required string UserId { get; set; }
    }
}
