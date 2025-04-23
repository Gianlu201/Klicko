using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Voucher
{
    public class EditVoucherRequestDto
    {
        [Required]
        public required bool IsUsed { get; set; }

        public DateTime? ReservationDate { get; set; }
    }
}
