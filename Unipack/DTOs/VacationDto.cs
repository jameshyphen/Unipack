using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Negative ID value")]
        [Required]
        public int VacationId { get; set; }
        [Required]
        public string Name { get; set; }
        public UserDto Author { get; set; }
        public ICollection<VacationLocationDto> Locations { get; set; }
        //public ICollection<VacationItemDto> VacationItems { get; set; }

        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }
        [Required]
        public DateTime DateReturn { get; set; }
    }
}
