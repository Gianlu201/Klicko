﻿using System.ComponentModel.DataAnnotations;

namespace Klicko_be.DTOs.Image
{
    public class CreateImageRequestDto
    {
        [Required]
        public required string Url { get; set; }
    }
}
