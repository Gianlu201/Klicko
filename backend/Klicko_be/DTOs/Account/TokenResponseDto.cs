namespace Klicko_be.DTOs.Account
{
    public class TokenResponseDto
    {
        public required string Token { get; set; }
        public required DateTime Expires { get; set; }
    }
}
