using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.Shared.DTOs
{
    public class PlaneDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TypePlaneId { get; set; }

        [Required]
        public string Created { get; set; }

        [Required]
        public string LifeTime { get; set; }
    }
}
