using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Experience
{
    public class GetExperiencesListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<ExperienceDto>? Experiences { get; set; }
    }
}
