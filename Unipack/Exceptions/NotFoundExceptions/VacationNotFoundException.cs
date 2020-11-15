using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class VacationNotFoundException : Exception
    {
        public VacationNotFoundException(int id)
            : base($"The requested vacation with the following ID={id} has not been found")
        {
        }
    }
}
