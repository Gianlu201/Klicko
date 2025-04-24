using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.Models.Auth;

namespace Klicko_be.DTOs.FidelityCard
{
    public class FidelityCardDto
    {
        [Required]
        public Guid FidelityCardId { get; set; }

        public string? CardNumber { get; set; }

        [Required]
        public required int Points { get; set; } = 0;

        [Required]
        public required int AvailablePoints { get; set; } = 0;

        [Required]
        public required string UserId { get; set; }

        // navigazione
        [ForeignKey(nameof(UserId))]
        public UserSimpleDto? User { get; set; }
    }
}
