using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.Category;
using Klicko_be.Models.Auth;

namespace Klicko_be.DTOs.Voucher
{
    public class VoucherDto
    {
        [Required]
        public Guid VoucherId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required Guid CategoryId { get; set; }

        [Required]
        public required string Duration { get; set; }

        [Required]
        public required string Place { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required string Organiser { get; set; }

        [Required]
        public required bool IsFreeCancellable { get; set; }

        [Required]
        public required string VoucherCode { get; set; }

        [Required]
        public DateTime? ReservationDate { get; set; }

        [Required]
        public required bool IsUsed { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string? UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // navigazione
        [ForeignKey(nameof(CategoryId))]
        public CategorySimpleDto? Category { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserSimpleDto? User { get; set; }
    }
}
