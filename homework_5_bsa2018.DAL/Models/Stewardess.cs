using System;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Stewardess
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
