using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klicko_be.Models
{
    public class OrderExperience
    {
        [Key]
        public Guid OrderExperienceId { get; set; }

        [Required]
        public required string Title { get; set; }

        //public required Guid ExperienceId { get; set; }
        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        [Range(
            0.01,
            1000000,
            ErrorMessage = "Price must be greater than 0.01 and less than 1000000"
        )]
        public required decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        // navigazione
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
