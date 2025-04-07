using System.ComponentModel.DataAnnotations;
using Klicko_be.Models.Auth;

namespace Klicko_be.DTOs.Experience
{
    public class CreateExperienceRequestDto
    {
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
        public required string DescriptionShort { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required int MaxParticipants { get; set; }

        [Required]
        public required string Organiser { get; set; }

        [Required]
        public required bool IsFreeCancellable { get; set; }

        public string? IncludedDescription { get; set; }

        public int Sale { get; set; }

        public bool IsInEvidence { get; set; }

        public bool IsPopular { get; set; }

        public bool IsDeleted { get; set; }

        public string? CoverImage { get; set; }

        [Required]
        public required int ValidityInMonths { get; set; }
    }
}
