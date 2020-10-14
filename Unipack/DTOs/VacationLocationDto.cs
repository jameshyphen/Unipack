﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationLocationDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Error 404 Id below 0")]
        [Required]
        public int VacationLocationId { get; set; }

        public string CityName { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime DateArrival { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }

        public VacationLocationDto() { }
        public VacationLocationDto(int id,string countryName, DateTime addedOn, DateTime dateArrival, DateTime dateDeparture)
        {
            VacationLocationId = id;
            CountryName = countryName;
            AddedOn = addedOn;
            DateArrival = dateArrival;
            DateDeparture = dateDeparture;
        }
    }
}