using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class VacationItem
    {
        public int VacationItemId { get; set; }
        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        public int VacationListId { get; set; }

        [ForeignKey(nameof(VacationListId))]
        public VacationList VacationList { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }

        public VacationItem() { }
        public VacationItem(VacationList VacationList, Item Item) // etc
        {
            this.VacationList = VacationList;
            this.VacationItemId = VacationList.VacationListId;
            this.Item = Item;
            this.ItemId = Item.ItemId;
        }

    }
}
