using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Experience
{
    public class DeleteExperienceResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
