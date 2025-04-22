using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Order;

namespace Klicko_be.DTOs.OrderExperience
{
    public class OrderExperienceDto
    {
        [Required]
        public Guid OrderExperienceId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Discount { get; set; } = 0;

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
