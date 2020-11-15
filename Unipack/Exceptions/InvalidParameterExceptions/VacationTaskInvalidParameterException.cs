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
            : base($"You must provide a name, a deadline and a user to create a VacationTask.") { }
        public VacationTaskInvalidParameterException(string name)
            : base($"The following name {name} is not valid.") { }
        public VacationTaskInvalidParameterException(User user)
            : base($"The following user {user} is not valid.") { }
        public VacationTaskInvalidParameterException(DateTime deadline)
            : base($"The following deadline {deadline} is not valid.") { }
    }
}
