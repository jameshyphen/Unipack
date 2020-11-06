using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Enums;

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
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Priority Priority { get; set; }
    }
}
