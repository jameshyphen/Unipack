using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class VacationTask
    {
        public int VacationTaskId { get; set; }

        public string Name { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public Boolean completed;

        //public Taskpriority Priority;

        public VacationTask(string name, DateTime deadLine /*, TaskPriority priority*/ )
        {
            DeadLine = deadLine;
            Name = name;
            AddedOn = DateTime.Now;
            //Priority = priority
        }
    }
}