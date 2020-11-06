using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    interface ITaskService
    {
        IEnumerable<VacationTask> GetAllTasksByUserId(int userId);
        IEnumerable<VacationTask> GetAllTasksByVacationListId(int userId, int vacationId);
        VacationTask GetTaskById(int taskId);
        bool AddTaskToVacationList(VacationTask task, int vacationListId);
        bool DeleteTaskById(int taskId);
        bool UpdateTask(int taskId, VacationTask task);
    }
}
