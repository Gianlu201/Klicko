using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Category
{
    public class CreateCategoryRequestDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public string? Image { get; set; }
        public string? Icon { get; set; }
    }
}
