using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.Models.Auth;

namespace Klicko_be.Models
{
    public class Coupon
    {
        [Key]
        public Guid CouponId { get; set; }

        public int PercentualSaleAmount { get; set; } = 0;

        public int FixedSaleAmount { get; set; } = 0;

        [Required]
        public required bool IsActive { get; set; } = true;

        [Required]
        public required bool IsUniversal { get; set; } = false;

        public DateTime? ExpireDate { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        public required int MinimumAmount { get; set; }

        [Required]
        public required string UserId { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
