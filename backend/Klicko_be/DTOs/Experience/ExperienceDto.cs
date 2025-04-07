using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.CarryWith;
using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Image;
using Klicko_be.Models;
using Klicko_be.Models.Auth;

namespace Klicko_be.DTOs.Experience
{
    public class ExperienceDto
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

        [Required]
        public required string UserCreatorId { get; set; }

        [Required]
        public required string UserLastModifyId { get; set; }

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
        [ForeignKey(nameof(UserCreatorId))]
        public UserSimpleDto? UserCreator { get; set; }

        [ForeignKey(nameof(UserLastModifyId))]
        public UserSimpleDto? UserLastModify { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategorySimpleDto? Category { get; set; }

        public ICollection<ImageSimpleDto>? Images { get; set; }

        public ICollection<CarryWithSimpleDto>? CarryWiths { get; set; }
    }
}
