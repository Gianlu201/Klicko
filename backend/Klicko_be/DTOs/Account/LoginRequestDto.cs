namespace Klicko_be.DTOs.Account
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
