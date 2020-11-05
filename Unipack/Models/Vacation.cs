using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class Vacation
    {
        #region Properties
        public int VacationId { get; set; }
        public string Name { get; set; }
        public ICollection<VacationLocation> Locations { get; set; }
        public ICollection<VacationList> VacationLists { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateReturn { get; set; }
        public DateTime AddedOn { get; set; }
        #endregion

        #region Constructors
        public Vacation()
        {
            Locations = new HashSet<VacationLocation>();
            VacationLists = new HashSet<VacationList>();
            AddedOn = DateTime.Now;
        }

        public Vacation(string name): this()
        {
            Name = name;
        }

        public Vacation(string name, DateTime dateDeparture, DateTime dateReturn): this(name)
        {
            DateDeparture = dateDeparture;
            DateReturn = dateReturn;
        }
        #endregion

        #region Behavior

        public void AddList(VacationList list)
        {
            VacationLists.Add(list);
        }

        #endregion
    }
}
