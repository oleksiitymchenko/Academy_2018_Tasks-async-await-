using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Crew
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Pilot Pilot { get; set; }

        [Required]
        public virtual List<Stewardess> Stewardesses { get; set; }

    }
}
