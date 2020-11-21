using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class VacationLocationNotFoundException : Exception
    {
        public VacationLocationNotFoundException(int id)
            : base($"The requested location with the following ID={id} has not been found.")
        {
        }
    }
}
