using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klicko_be.Models
{
    public class OrderExperience
    {
        [Key]
        public Guid OrderExperienceId { get; set; }

        public required Guid ExperienceId { get; set; }
        public required Guid OrderId { get; set; }

        public int Quantity { get; set; } = 1;

        // navigazione
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [ForeignKey(nameof(ExperienceId))]
        public Experience? Experience { get; set; }
    }
}
