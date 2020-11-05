using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id) 
            : base($"The requested user with the following ID={id} has not been found") { }
    }
}
