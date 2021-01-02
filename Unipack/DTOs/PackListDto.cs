using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class PackListDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Ah yes, an Id below 0, not sure how you managed that but good job!")]
        [Required]
        public int PackListId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PackItemDto> Items { get; set; }
        public ICollection<PackTaskDto> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
        //Vacation props:
        //public ICollection<VacationLocationDto> Locations { get; set; }
    }
}
