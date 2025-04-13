using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.ApplicationRole
{
    public class RoleDto
    {
        [Required]
        public required string RoleId { get; set; }

        [Required]
        public required string RoleName { get; set; }
    }
}
