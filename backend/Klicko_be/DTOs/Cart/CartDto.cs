using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Experience;
using Klicko_be.Models;
using Klicko_be.Models.Auth;

namespace Klicko_be.DTOs.Cart
{
    public class CartDto
    {
        [Required]
        public Guid CartId { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public UserSimpleDto? User { get; set; }

        public ICollection<ExperienceForCartDto?>? CartExperiences { get; set; }
    }
}
