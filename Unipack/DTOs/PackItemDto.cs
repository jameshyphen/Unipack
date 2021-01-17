using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class PackItemDto
    {
        public int PackListId { get; set; }
        public PackListDto PackList { get; set; }
        public int ItemId { get; set; }
        public ItemDto Item { get; set; }
        public int Quantity { get; set; }
        public int PackedQuantity { get; set; }
    }
}
