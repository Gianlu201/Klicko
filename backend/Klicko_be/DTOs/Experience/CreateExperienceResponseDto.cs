using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Experience
{
    public class CreateExperienceResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
