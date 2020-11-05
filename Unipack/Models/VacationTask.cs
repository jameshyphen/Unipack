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
        public User AuthorUser { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public Boolean Completed;

        public Priority Priority;

        #endregion

        #region Constructors

        public VacationTask(string name)
        {
            Name = name;
            AddedOn = DateTime.Now;
        }

        public VacationTask(string name, DateTime deadLine): this(name)
        {
            DeadLine = deadLine;
        }

        public VacationTask(string name, User user) : this(name)
        {
            AuthorUser = user;
        }

        public VacationTask(string name, User user, DateTime deadLine) : this(name, user)
        {
            DeadLine = deadLine;
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