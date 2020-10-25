using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Unipack.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int id)
            : base($"The requested item with the following ID={id} has not been found")
        {
        }
    }
}
