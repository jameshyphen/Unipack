using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Unipack.Enums;

namespace Unipack.Models
{
    public class VacationTask
    {
        #region Properties

        public int VacationTaskId { get; set; }

        public string Name { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public Boolean Completed;

        public Priority Priority;

        #endregion

        #region Constructors

        public VacationTask(string name, DateTime deadLine)
        {
            DeadLine = deadLine;
            Name = name;
            AddedOn = DateTime.Now;

        }

        #endregion

        #region Behavior

        // Increase or decrease priority
        public void IncreasePriority()
        {
            if ((int) (Priority) >= 0 && (int) (Priority) < 3)
                Priority = (Priority) ((int) (Priority)++);
        }
        public void DecreasePriority()
        {
            if ((int)(Priority) > 0 && (int)(Priority) <= 3)
                Priority = (Priority)((int)(Priority)--);
        }

        #endregion
    }
}