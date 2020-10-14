using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class Item
    {
        public int ItemId {get;set;}
        public string Name { get; set; }
        public DateTime AddedOn { get; set; }
        //public ItemCategory Category { get; set; }
        public Item(){}
        public Item(string name, DateTime addedOn/*, ItemCategory category*/)
        {
            Name = name;
            AddedOn = addedOn;
            //Category = category;
        }
    }
}
