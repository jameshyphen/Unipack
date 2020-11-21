using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationTaskInvalidParameterException : Exception
    {
        public VacationTaskInvalidParameterException()
            : base($"You must provide a name Vacation Task.") { }
        public VacationTaskInvalidParameterException(DateTime deadline)
            : base($"The deadline {deadline.Date} cannot be in the past.") { }
    }
}
