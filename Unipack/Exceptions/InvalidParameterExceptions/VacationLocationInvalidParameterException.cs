using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationLocationInvalidParameterException : Exception
    {
        public VacationLocationInvalidParameterException()
            : base($"Something went wrong instantiating the vacation location.") { }
        public VacationLocationInvalidParameterException(string name)
            : base($"The following country name {name} is not valid.") { }
        public VacationLocationInvalidParameterException(DateTime departureDate, DateTime returnDate)
            : base($"The return date {returnDate.Date} cannot be before the departure date {departureDate.Date}.") { }

    }
}
