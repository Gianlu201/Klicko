using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.Models.Auth;

namespace Klicko_be.Models
{
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        [Required]
        public required string UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public ICollection<CartExperience>? CartExperiences { get; set; }
    }
}
