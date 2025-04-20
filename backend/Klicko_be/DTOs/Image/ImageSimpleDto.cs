namespace Klicko_be.DTOs.Image
{
    public class ImageSimpleDto
    {
        public Guid ImageId { get; set; }
        public required string Url { get; set; }
    }
}
