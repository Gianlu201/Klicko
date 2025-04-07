using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.Models.Auth;

namespace Klicko_be.Models
{
    public class Experience
    {
        [Key]
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
        public required string DescriptionShort { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required int MaxParticipants { get; set; }

        [Required]
        public required string Organiser { get; set; }

        public DateTime LoadingDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public ApplicationUser? UserCreator { get; set; }

        [Required]
        public ApplicationUser? UserLastModify { get; set; }

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

        // navigazione
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public ICollection<Image>? Images { get; set; }

        public ICollection<CarryWith>? CarryWiths { get; set; }

        public ICollection<OrderExperience>? OrderExperiences { get; set; }

        public ICollection<CartExperience>? CartExperiences { get; set; }
    }
}
