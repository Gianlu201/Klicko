using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Experience;
using Klicko_be.Models;
using Klicko_be.Models.Auth;

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
        public required decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        // navigazione
        public ICollection<ExperienceForOrdersDto>? Experiences { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserSimpleDto? User { get; set; }
    }
}
