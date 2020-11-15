using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class ItemInvalidParameterException : Exception
    {
        public ItemInvalidParameterException()
            : base($"You must provide a name, category and user to create an Item.")
        {
        }
        public ItemInvalidParameterException(string name)
            : base($"The following name {name} is not valid.")
        {
        }
        public ItemInvalidParameterException(Category category)
            : base($"The following category {category} is not valid.")
        {
        }
        public ItemInvalidParameterException(User user)
            : base($"The following user {user} is not valid.")
        {
        }
    }
}
