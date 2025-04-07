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

        [Required]
        public required decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        // navigazione
        public ICollection<OrderExperience>? OrderExperiences { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
