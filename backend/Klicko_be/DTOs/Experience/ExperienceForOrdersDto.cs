using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Category;

namespace Klicko_be.DTOs.Experience
{
    public class ExperienceForOrdersDto
    {
        [Required]
        public required Guid ExperienceId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required Guid CategoryId { get; set; }

        [Required]
        public required string Duration { get; set; }

        [Required]
        public required string Place { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required int Quantity { get; set; } = 1;

        [Required]
        public required string DescriptionShort { get; set; }

        [Required]
        public required int MaxParticipants { get; set; }

        [Required]
        public required string Organiser { get; set; }

        public string? CoverImage { get; set; }

        [Required]
        public required int ValidityInMonths { get; set; }

        // navigazione
        [ForeignKey(nameof(CategoryId))]
        public CategorySimpleDto? Category { get; set; }
    }
}
