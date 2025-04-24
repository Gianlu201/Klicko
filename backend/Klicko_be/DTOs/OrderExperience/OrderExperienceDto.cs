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
        public required decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
