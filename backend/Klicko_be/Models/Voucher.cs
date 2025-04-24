using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.Models.Auth;

namespace Klicko_be.Models
{
    public class Voucher
    {
        [Key]
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

        public string? VoucherCode { get; set; }

        public DateTime? ReservationDate { get; set; }

        [Required]
        public required bool IsUsed { get; set; } = false;

        [Required]
        public DateTime ExpirationDate { get; set; }

        public string? UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public required Guid OrderId { get; set; }

        // navigazione
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
