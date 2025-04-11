using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Category;

namespace Klicko_be.DTOs.Experience
{
    public class ExperienceForCartDto
    {
        [Required]
        public required Guid ExperienceId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required Guid CategoryId { get; set; }

        [Required]
        public required string Place { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required bool IsFreeCancellable { get; set; }

        public int Sale { get; set; }

        public string? CoverImage { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategorySimpleDto? Category { get; set; }
    }
}
