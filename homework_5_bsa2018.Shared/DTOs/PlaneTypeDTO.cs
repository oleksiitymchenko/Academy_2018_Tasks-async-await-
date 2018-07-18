using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.Shared.DTOs
{
    public class PlaneTypeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Places { get; set; }

        [Required]
        public double Carrying { get; set; }
    }
}
