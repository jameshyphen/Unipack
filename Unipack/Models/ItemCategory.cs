using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class ItemCategory
    {
        public int ItemCategoryID { get; set; }

        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public DateTime AddedOn { get; set; }

        public ItemCategory(string name )
        {
            Name = name;
            AddedOn = DateTime.Now;
        }

        public ItemCategory()
        {
        }



    }
}
