using System;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.DAL.Models
{
    public class Plane
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual PlaneType TypePlane { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public TimeSpan LifeTime { get; set; }
    }
}
