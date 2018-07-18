using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 3)]
        [Required]
        public string Number { get; set; }

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public string FinishPoint { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public virtual List<Ticket> Tickets { get; set; }
    }
}
