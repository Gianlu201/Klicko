using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Category
{
    public class GetCategoriesListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<CategorySimpleDto>? Categories { get; set; }
    }
}
