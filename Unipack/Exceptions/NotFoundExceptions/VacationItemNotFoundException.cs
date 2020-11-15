using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class VacationItemNotFoundException : Exception
    {
        public VacationItemNotFoundException(int itemId, int listId)
            : base($"The requested item with the following ID={itemId} has not been found in the list with ID={listId}")
        {
        }
    }
}
