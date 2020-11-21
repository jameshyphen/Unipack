using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationLocationInvalidParameterException : Exception
    {
        public VacationLocationInvalidParameterException()
            : base($"You must provide a countryname, addeddate, departuredate and arrivaldate to create a Vacation Location.") { }
        public VacationLocationInvalidParameterException(string name)
            : base($"The following countryname {name} is not valid.") { }
        public VacationLocationInvalidParameterException(DateTime date)
            : base($"Either the following departuredate, addeddate or arrivaldate {date} is not valid.") { }

    }
}
