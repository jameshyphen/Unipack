using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class UserInvalidParameterException : Exception
    {
        public UserInvalidParameterException()
            : base($"You must provide a firstname, lastname and email to create a User. Username is optional")
        {
        }
        public UserInvalidParameterException(string value)
            : base($"Either the following firstname, lastname,email or username {value} is not valid.") { }
    }
}
