using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class VacationLocation
    {
        public int VacationLocationId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
        public VacationLocation() { }
        //public VacationLocation(string countryName, DateTime dateDeparture, DateTime dateArrival)
        //{
        //    CountryName = countryName;
        //    AddedOn = DateTime.Now;
        //    DateArrival = dateArrival;
        //    DateDeparture = dateDeparture;
        //}
    }
}
