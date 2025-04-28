using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Klicko_be.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid CartId { get; set; }

        public Guid FidelityCardId { get; set; }

        // navigazione
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }

        [ForeignKey(nameof(FidelityCardId))]
        public FidelityCard? FidelityCard { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Experience>? ExperiencesCreated { get; set; }

        public ICollection<Voucher>? Vouchers { get; set; }

        public ICollection<Coupon>? Coupons { get; set; }
    }
}
