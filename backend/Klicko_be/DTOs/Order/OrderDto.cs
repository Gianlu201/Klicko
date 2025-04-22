using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.OrderExperience;

namespace Klicko_be.DTOs.Order
{
    public class OrderDto
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string State { get; set; }

        [Required]
        public decimal SubTotalPrice { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal TotalDiscount { get; set; }

        [Required]
        public required decimal TotalPrice { get; set; }

        [Required]
        public decimal ShippingPrice { get; set; }

        // navigazione
        public ICollection<OrderExperienceDto>? OrderExperiences { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserSimpleDto? User { get; set; }
    }
}
