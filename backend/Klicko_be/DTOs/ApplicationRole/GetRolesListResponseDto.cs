using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.ApplicationRole
{
    public class GetRolesListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<RoleDto>? Roles { get; set; }
    }
}
