using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class ItemCategory
    {
        public int ItemCategoryId { get; set; }

        public string Name { get; set; }
        public User AuthorUser { get; set; }
        public DateTime AddedOn { get; set; }


        public ItemCategory(string name, User author)
        {
            Name = name;
            AuthorUser = author;
            AddedOn = DateTime.Now;
        }





    }
}
