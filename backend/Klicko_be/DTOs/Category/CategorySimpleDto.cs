namespace Klicko_be.DTOs.Category
{
    public class CategorySimpleDto
    {
        public Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
    }
}
