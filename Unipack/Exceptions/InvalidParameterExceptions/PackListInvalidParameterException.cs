using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class PackListInvalidParameterException : Exception
    {
        public PackListInvalidParameterException()
            :base($"You must provide a name and a user to create a vacation list.") {}
        public PackListInvalidParameterException(string name)
            : base($"The following name {name} is not valid.") { }
        public PackListInvalidParameterException(User user)
            : base($"The following user {user} is not valid.") { }
    }
}
