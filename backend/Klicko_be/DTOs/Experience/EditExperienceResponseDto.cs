using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Experience
{
    public class EditExperienceResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
