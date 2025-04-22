using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.Models.Auth;

namespace Klicko_be.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string State { get; set; }

        public decimal SubTotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal ShippingPrice { get; set; }

        // navigazione
        public ICollection<OrderExperience>? OrderExperiences { get; set; }

        public ICollection<Voucher>? Vouchers { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
