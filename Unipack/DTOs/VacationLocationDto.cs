using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationLocationDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Negative ID exception")]
        [Required]
        public int VacationLocationId { get; set; }

        public string CityName { get; set; } = null! ;

        [Required]
        public string CountryName { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime DateArrival { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }
    }
}
