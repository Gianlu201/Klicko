using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Account
{
    public class EditUserRoleResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
