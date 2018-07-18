using System;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Departure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public virtual Crew Crew { get; set; }

        [Required]
        public virtual Plane Plane { get; set; }
    }
}
