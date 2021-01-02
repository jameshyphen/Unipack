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
        [Range(0, int.MaxValue, ErrorMessage = "Negative ID")]
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
