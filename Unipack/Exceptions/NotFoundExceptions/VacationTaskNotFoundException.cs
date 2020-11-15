using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class VacationTaskNotFoundException: Exception
    {
        public VacationTaskNotFoundException(int id)
            : base($"The requested tast with the following ID={id} has not been found.")
        {
        }
    }
}
