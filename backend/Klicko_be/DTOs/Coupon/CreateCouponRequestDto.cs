using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Coupon
{
    public class CreateCouponRequestDto
    {
        [Required]
        public int PercentualSaleAmount { get; set; }

        [Required]
        public int FixedSaleAmount { get; set; }

        [Required]
        public required bool IsActive { get; set; }

        [Required]
        public required bool IsUniversal { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Required]
        public required int MinimumAmount { get; set; }

        [Required]
        public required string UserId { get; set; }
    }
}
