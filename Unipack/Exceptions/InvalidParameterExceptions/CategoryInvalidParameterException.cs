using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class CategoryInvalidParameterException: Exception
    {
        public CategoryInvalidParameterException()
            : base($"You must provide a name and a user to create a Category.") { }
        public CategoryInvalidParameterException(string name)
            : base($"The following name {name} is not valid.") { }
        public CategoryInvalidParameterException(User user)
            : base($"The following user {user} is not valid.") { }

    }
}
