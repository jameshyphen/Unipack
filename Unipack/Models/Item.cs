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
        public DateTime AddedOn { get; set; }
        public Priority Priority { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ItemCategory Category { get; set; }
        public int CategoryId { get; set; }

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
        public Item(string name, ItemCategory category) : this(name)
        {
            Category = category;
        }

        #endregion
    }
}
