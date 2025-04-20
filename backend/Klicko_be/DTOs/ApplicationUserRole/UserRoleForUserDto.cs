using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.ApplicationUserRole
{
    public class UserRoleForUserDto
    {
        [Required]
        public required Guid UserRoleId { get; set; }

        [Required]
        public required string RoleId { get; set; }

        [Required]
        public required string? RoleName { get; set; }
    }
}
