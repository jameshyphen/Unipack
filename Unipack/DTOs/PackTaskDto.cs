using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Enums;

namespace Unipack.DTOs
{
    public class PackTaskDto
    {
        public string Name { get; set; }
        public bool Completed { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime DeadLine { get; set; }
        public Priority Priority { get; set; }
    }
}
