using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Payment
{
    public class PaymentRequestDto
    {
        [Required]
        public required long Amount { get; set; }
    }
}
