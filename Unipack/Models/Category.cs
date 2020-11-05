using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class Category
    {
        #region Properties

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public User AuthorUser { get; set; }
        public ICollection<Item> Items { get; set; }
        public DateTime AddedOn { get; set; }

        #endregion

        #region Constructors

        public Category()
        {
            Items = new HashSet<Item>();
            AddedOn = DateTime.Now;
        }
        public Category(string name) : this()
        {
            Name = name;
        }
        public Category(string name, User user) : this(name)
        {
            this.AuthorUser = user;
        }
        #endregion

        #region Behavior


        #endregion
    }
}
