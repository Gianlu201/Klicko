using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.CarryWith
{
    public class CreateCarryWithRequestDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
