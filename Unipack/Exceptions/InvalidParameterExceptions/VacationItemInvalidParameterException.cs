using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Exceptions.InvalidParameterExceptions
{
    public class VacationItemInvalidParameterException : Exception
    {
        public VacationItemInvalidParameterException()
            :base($"You must provide an item and a vacationlist to make a relation between the 2."){}
        public VacationItemInvalidParameterException(VacationList list)
            : base($"The following list {list} is not valid.") { }
        public VacationItemInvalidParameterException(Item item)
            : base($"The following item {item} is not valid.") { }
    }
}
