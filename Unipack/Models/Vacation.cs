using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class Vacation
    {
        public string Name { get; set; }
        public ICollection<VacationLocation> Locations { get; set; }
        public ICollection<VacationList> VacationLists { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateReturn { get; set; }
        public DateTime AddedOn { get; set; }

        public Vacation()
        {
            Locations = new HashSet<VacationLocation>();
        }
    }
}
