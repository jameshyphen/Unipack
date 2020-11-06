using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Exceptions.NotFoundExceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(int id)
            : base($"The requested task with the following ID={id} has not been found")
        {
        }
    }
}
