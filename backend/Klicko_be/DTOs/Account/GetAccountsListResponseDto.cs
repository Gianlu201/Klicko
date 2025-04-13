using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Account
{
    public class GetAccountsListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<AccountDto>? Accounts { get; set; }
    }
}
