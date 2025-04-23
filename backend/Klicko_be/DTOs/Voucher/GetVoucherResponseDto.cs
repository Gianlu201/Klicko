using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Voucher
{
    public class GetVoucherResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required VoucherDto? Voucher { get; set; }
    }
}
