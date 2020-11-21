using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationListInvalidParameterException : Exception
    {
        public VacationListInvalidParameterException()
            :base($"You must provide a name and a user to create a vacation list.") {}
        public VacationListInvalidParameterException(string name)
            : base($"The following name {name} is not valid.") { }
        public VacationListInvalidParameterException(User user)
            : base($"The following user {user} is not valid.") { }
    }
}
