using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 2)]
        [Required]
        public string FlightNumber { get; set; }
    }
}
