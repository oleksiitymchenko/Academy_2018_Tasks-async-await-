using System;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.Shared.DTOs
{
    public class StewardessDTO
    {
        [Required]
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(128, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
