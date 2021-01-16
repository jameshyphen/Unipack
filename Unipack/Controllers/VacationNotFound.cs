using System;
using System.Runtime.Serialization;

namespace Unipack.Controllers
{
    [Serializable]
    internal class VacationNotFound : Exception
    {
        public VacationNotFound()
        {
        }

        public VacationNotFound(string message) : base(message)
        {
        }

        public VacationNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VacationNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}