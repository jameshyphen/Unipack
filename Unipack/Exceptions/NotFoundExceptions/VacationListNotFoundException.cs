using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions
{
    public class VacationListNotFoundException : Exception
    {
        public VacationListNotFoundException(int id)
            : base($"The requested vacation list with the following ID={id} has not been found")
        {
        }
    }
}
