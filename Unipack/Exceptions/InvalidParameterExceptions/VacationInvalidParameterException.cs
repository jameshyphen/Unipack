using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationInvalidParameterException : Exception
    {
        public VacationInvalidParameterException()
            : base($"You must provide a name, depaturedate and returndate to create a vacation.") { }
        public VacationInvalidParameterException(string name)
            : base($"The following name {name} is not valid") { }
        public VacationInvalidParameterException(DateTime date)
            : base($"Either the following departuredate or returndate {date} is not valid.") { }
    }
}
