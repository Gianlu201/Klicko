using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Order
{
    public class EditOrderStateRequestDto
    {
        [Required]
        public required string State { get; set; }
    }
}
