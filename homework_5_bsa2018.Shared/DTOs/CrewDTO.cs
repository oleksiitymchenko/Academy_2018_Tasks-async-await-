using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace homework_5_bsa2018.Shared.DTOs
{
    public class CrewDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PilotId { get; set; }
        [Required]
        public List<int> StewardressIds { get; set; }
    }
}
