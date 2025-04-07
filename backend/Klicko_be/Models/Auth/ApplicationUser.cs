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

        // navigazione
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        [InverseProperty(nameof(Cart.User))]
        public Cart? Cart { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
