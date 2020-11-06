using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Data.Interfaces;
using Unipack.Exceptions;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class TaskService : ITaskService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<VacationList> _lists;
        private readonly DbSet<VacationTask> _tasks;

        public TaskService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._lists = context.VacationLists;
            this._tasks = context.VacationTasks;
        }

        public bool AddTaskToVacationList(VacationTask task, int vacationListId)
        {
            var list = _lists.FirstOrDefault(l => l.VacationListId == vacationListId) ??
                                throw new VacationListNotFoundException(vacationListId);

            list.Tasks.Add(task);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteTaskById(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.VacationTaskId == taskId) ?? throw new TaskNotFoundException(taskId);
            _tasks.Remove(task);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<VacationTask> GetAllTasksByUserId(int userId)
        {
            var tasks = _tasks
                .Where(t => t.Author.UserId == userId)
                .ToList();
            return tasks;
        }

        public IEnumerable<VacationTask> GetAllTasksByVacationListId(int userId, int vacationId)
        {
            var list = _lists
                .Where(l => l.AuthorUser.UserId == userId)
                .Include(l => l.Tasks)
                .FirstOrDefault();
            return list.Tasks;
        }

        public VacationTask GetTaskById(int taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.VacationTaskId == taskId) ?? throw new TaskNotFoundException(taskId);
            return task;
        }

        public bool UpdateTask(int taskId, VacationTask task)
        {
            var toBeUpdatedTask = _tasks.FirstOrDefault(x => x.VacationTaskId == taskId) ??
                                 throw new TaskNotFoundException(taskId);
            toBeUpdatedTask.Priority = task.Priority;
            toBeUpdatedTask.DeadLine = task.DeadLine;
            toBeUpdatedTask.Completed = task.Completed;

            _tasks.Update(toBeUpdatedTask);
            return _context.SaveChanges() != 0;
        }
    }
}
