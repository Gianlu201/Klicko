using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Voucher
{
    public class EditVoucherResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
