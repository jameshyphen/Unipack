using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class PackItem
    {
        public int PackItemId { get; set; }
        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        public int PackListId { get; set; }

        [ForeignKey(nameof(PackListId))]
        public PackList PackList { get; set; }
        public int PackedQuantity { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedOn { get; set; }

        public PackItem() {
            AddedOn = DateTime.Now;
        }
        public PackItem(PackList VacationList, Item Item) :this() // etc
        {
            this.PackList = VacationList;
            this.PackItemId = VacationList.PackListId;
            this.Item = Item;
            this.ItemId = Item.ItemId;
        }

    }
}
