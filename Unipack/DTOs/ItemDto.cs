using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class ItemDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Ah yes, an Id below 0, not sure how you managed that but good job!")]
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        //[Required]
        //public ItemCategoryDto Category { get; set; }

        public ItemDto(){}
        public ItemDto(int itemId, string name, DateTime addedOn/*, ItemCategoryDto category*/)
        {
            ItemId = itemId;
            Name = name;
            AddedOn = addedOn;
            //Category = category;
        }
    }
}
