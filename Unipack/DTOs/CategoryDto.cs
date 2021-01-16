using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Enums;
using Unipack.Models;

namespace Unipack.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public UserDto Author { get; set; }
        public DateTime AddedOn { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
