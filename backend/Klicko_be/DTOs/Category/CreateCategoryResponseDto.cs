using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Category
{
    public class CreateCategoryResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
