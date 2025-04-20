using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Experience
{
    public class GetExperienceResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required ExperienceDto? Experience { get; set; }
    }
}
