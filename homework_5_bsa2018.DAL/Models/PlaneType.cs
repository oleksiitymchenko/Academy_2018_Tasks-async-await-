
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class PlaneType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Places { get; set; }

        [Required]
        public double Carrying { get; set; }
    }
}
