﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class VacationListDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Ah yes, an Id below 0, not sure how you managed that but good job!")]
        [Required]
        public int VacationListId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<VacationLocationDto> Locations { get; set; }
        //public ICollection<VacationItemDto> Items { get; set; }
        //public ICollection<VacationTaskDto> Tasks { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }

        public VacationListDto() { }
        public VacationListDto(int id, string name, DateTime added)
        {
            VacationListId = id;
            Name = name;
            AddedOn = added;
        }
    }
}
