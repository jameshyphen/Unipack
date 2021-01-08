using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Unipack.Models
{
    public class PackList
    {
        #region Properties

        public int PackListId { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public ICollection<PackItem> Items { get; set; }
        public ICollection<PackTask> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
        
        #endregion

        #region Constructors

        public PackList()
        {
            Items = new HashSet<PackItem>();
            Tasks = new HashSet<PackTask>();
            AddedOn = DateTime.Now;
        }

        public PackList(string name) : this()
        {
            Name = name;
        }

        public PackList(string name, User user) : this(name)
        {
            Author = user;
        }

        #endregion

        #region Behavior

        public void AddItem(Item item)
        {
            PackItem vacItem = new PackItem(this, item);
            Items.Add(vacItem);
        }

        #endregion

    }
}
