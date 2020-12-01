using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Enums;

namespace Unipack.Models
{
    public class Item
    {
        #region Properties

        public int ItemId { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public DateTime AddedOn { get; set; }
        public Priority Priority { get; set; }
        public Category Category { get; set; }
        // Random test comment
        #endregion

        #region Constructors

        public Item()
        {
            AddedOn = DateTime.Now;
        }
        public Item(string name) : this()
        {
            Name = name;
        }
        public Item(string name, Category category) : this(name)
        {
            Category = category;
        }

        public Item(string name, User user) : this(name)
        {
            Author = user;
        }
        public Item(string name, Category category, User user) : this(name, category)
        {
            Author = user;
        }
        #endregion
    }
}
