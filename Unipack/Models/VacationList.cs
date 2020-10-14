using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Unipack.Models
{
    public class VacationList
    {
        public int VacationListId { get; set; }
        public string Name { get; set; }
        public ICollection<VacationLocation> Locations { get; set; }
        public ICollection<VacationItem> Items { get; set; }
        public ICollection<VacationTask> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }

        public VacationList()
        {
            Locations = new HashSet<VacationLocation>();
            Items = new HashSet<VacationItem>();
            Tasks = new HashSet<VacationTask>();
        }
    }
}
