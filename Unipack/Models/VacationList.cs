﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Unipack.Models
{
    public class VacationList
    {
        #region Properties

        public int VacationListId { get; set; }
        public string Name { get; set; }
        public ICollection<VacationItem> Items { get; set; }
        public ICollection<VacationTask> Tasks { get; set; }
        public DateTime AddedOn { get; set; }
        
        #endregion

        #region Constructors

        public VacationList()
        {
            Items = new HashSet<VacationItem>();
            Tasks = new HashSet<VacationTask>();
            AddedOn = DateTime.Now;
        }

        public VacationList(string name) : this()
        {
            Name = name;
        }

        #endregion

        #region Behavior

        public void AddItem(Item item)
        {
            VacationItem vacItem = new VacationItem(this, item);
            Items.Add(vacItem);
        }

        #endregion

    }
}
