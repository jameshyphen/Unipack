using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class ItemCategory
    {
        #region Properties

        public int ItemCategoryId { get; set; }

        public string Name { get; set; }
        public User AuthorUser { get; set; }
        public DateTime AddedOn { get; set; }

        #endregion

        #region Constructors

        public ItemCategory()
        {
            AddedOn = DateTime.Now;
        }
        public ItemCategory(string name, User author): this()
        {
            Name = name;
            AuthorUser = author;
        }

        #endregion

    }
}
