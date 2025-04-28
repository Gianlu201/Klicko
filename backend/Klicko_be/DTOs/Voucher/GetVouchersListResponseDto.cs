using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Voucher
{
    public class GetVouchersListResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
        public required List<VoucherDto>? Vouchers { get; set; }
    }
}
