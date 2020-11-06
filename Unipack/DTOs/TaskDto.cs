using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Enums;
using Unipack.Models;

namespace Unipack.DTOs
{
    public class TaskDto
    {

        [Range(0, int.MaxValue, ErrorMessage = "Ah yes, an Id below 0, not sure how you managed that but good job!")]
        [Required]
        public int VacationTaskId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }

        public Boolean Completed;

        public Priority Priority;


    }
}
