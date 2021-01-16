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
        public User Author { get; set; }
        public ICollection<VacationLocation> Locations { get; set; }
        public ICollection<PackList> PackLists { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateReturn { get; set; }
        public DateTime AddedOn { get; set; }
        #endregion

        #region Constructors
        public Vacation()
        {
            Locations = new HashSet<VacationLocation>();
            PackLists = new HashSet<PackList>();
            AddedOn = DateTime.Now;
        }

        public Vacation(string name, User author): this()
        {
            Name = name;
            Author = author;
        }

        public Vacation(string name, User author, DateTime dateDeparture, DateTime dateReturn): this(name, author)
        {
            DateDeparture = dateDeparture;
            DateReturn = dateReturn;
        }
        #endregion

        #region Behavior

        public void AddList(PackList list)
        {
            PackLists.Add(list);
        }

        #endregion
    }
}
