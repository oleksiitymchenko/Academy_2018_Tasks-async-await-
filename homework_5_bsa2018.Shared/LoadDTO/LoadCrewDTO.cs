using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;

namespace homework_5_bsa2018.Shared.LoadDTO
{
    public class LoadCrewDTO
    {
        public int id { get; set; }
        public List<Pilot> pilot { get; set; }
        public List<Stewardess> stewardess { get; set; }

    }
}
