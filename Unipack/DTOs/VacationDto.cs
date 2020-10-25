using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Error 404 Id below 0")]
        [Required]
        public int VacationId { get; set; }
        public ICollection<VacationLocationDto> Locations { get; set; }
        // public ICollection<VacationItemDto> VacationItems { get; set; }

        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }
        [Required]
        public DateTime DateReturn { get; set; }

        public VacationDto() { }
        public VacationDto(int id, string countryName, DateTime addedOn, DateTime dateDeparture, DateTime dateReturn)
        {
            VacationId = id;
            AddedOn = addedOn;
            DateDeparture = dateDeparture;
            DateReturn = dateReturn;
        }
    }
}
