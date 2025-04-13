using System.ComponentModel.DataAnnotations;
using Klicko_be.DTOs.ApplicationUserRole;

namespace Klicko_be.DTOs.Account
{
    public class AccountDto
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required DateTime RegistrationDate { get; set; }

        [Required]
        public required UserRoleForUserDto? UserRole { get; set; }
    }
}
