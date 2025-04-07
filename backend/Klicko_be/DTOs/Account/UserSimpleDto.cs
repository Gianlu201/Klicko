namespace Klicko_be.DTOs.Account
{
    public class UserSimpleDto
    {
        public required string UserId { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string? Email { get; set; }
    }
}
