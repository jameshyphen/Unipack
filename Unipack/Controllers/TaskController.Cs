using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;

namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public TaskController(IUserService userService, UserManager<IdentityUser> userManager, ILogger<TaskController> logger)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns all Tasks created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasks()
        {
            try
            {
                var user = await GetCurrentUser();
                var result = _taskService.GetAllTasksByUserId(user.UserId)
                    .Select(x => new TaskDto
                    {
                        Name = x.Name,
                        Priority = x.Priority,
                        DeadLine = x.DeadLine,
                        Completed = x.Completed, 
                        VacationTaskId = x.VacationTaskId,
                        AddedOn = x.AddedOn
                    }
                    ).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }
        }

        /// <summary>
        /// Finds all Tasks with the specified Vacation id.
        /// </summary>
        /// <param name="vacationListId">The id of the Vacation you're looking to get all Tasks from.</param>  
        [HttpGet("{vacationListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasksByVacationList(int vacationListId)
        {
            try
            {
                var user = await GetCurrentUser();
                var result = _taskService.GetAllTasksByVacationListId(user.UserId, vacationListId)
                    .Select(x => new TaskDto
                    {
                        Name = x.Name,
                        Priority = x.Priority,
                        DeadLine = x.DeadLine,
                        Completed = x.Completed,
                        VacationTaskId = x.VacationTaskId,
                        AddedOn = x.AddedOn
                    }
                    ).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return BadRequest(new
                {
                    message = "Internal server error: " + e.Message
                });
            }
        }

        /// <summary>
        /// Finds a Task with the specified id.
        /// </summary>
        /// <param name="taskId">The id of the Task you're looking to get.</param>  
        [HttpGet("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<TaskDto> GetTask(int taskId)
        {
            try
            {
                var task = _taskService.GetTaskById(taskId);
                var result = new TaskDto
                {
                    Name = task.Name,
                    Priority = task.Priority,
                    DeadLine = task.DeadLine,
                    Completed = task.Completed,
                    VacationTaskId = task.VacationTaskId,
                    AddedOn = task.AddedOn
                };
                return Ok(result);

            }
            catch (TaskNotFoundException e)
            {
                return NotFound(new { message = "Not found: " + e.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates an Task with the passed on Task DTO model to vacation.
        /// </summary>
        /// <param name="taskDto">The Task DTO model of the Task to be created.</param>  
        [HttpPost("{vacationListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddTaskToVacationList([FromBody] TaskDto taskDto, int vacationListId)
        {
            try
            {
                var user = await GetCurrentUser();
                var task = new VacationTask(taskDto.Name)
                {
                    AddedOn = taskDto.AddedOn,
                    Author = user,
                    VacationTaskId = taskDto.VacationTaskId,
                    Completed = taskDto.Completed,
                    DeadLine = taskDto.DeadLine,
                    Priority = taskDto.Priority
                };
                if (_taskService.AddTaskToVacationList(task, vacationListId))
                    return Ok(true);
                throw new Exception("Something went wrong, item has not been created.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates an Task with the passed on Task DTO Model.
        /// </summary>
        /// <param name="taskId">The Id of the Task to be updated.</param>  
        /// <param name="taskDto">The Task DTO Model of the Task to be updated.</param>  
        [HttpPut("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateTask(int taskId, [FromBody] TaskDto taskDto)
        {
            try
            {
                var user = await GetCurrentUser();
                var task = new VacationTask(taskDto.Name)
                {
                    AddedOn = taskDto.AddedOn,
                    Author = user,
                    VacationTaskId = taskDto.VacationTaskId,
                    Completed = taskDto.Completed,
                    DeadLine = taskDto.DeadLine,
                    Priority = taskDto.Priority
                };
                if (_taskService.UpdateTask(taskId, task))
                    return Ok(true);
                throw new Exception("Something went wrong, item has not been updated.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an Task with the specified id.
        /// </summary>
        /// <param name="taskId">The Id of the Task to be updated.</param>  
        [HttpDelete("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteTask(int taskId)
        {
            try
            {
                var user = await GetCurrentUser();
                if (_taskService.GetTaskById(taskId).Author.UserId == user.UserId)
                {
                    if (_taskService.DeleteTaskById(taskId))
                        return Ok(true);
                }
                else
                {
                    return BadRequest("This item does not belong to your account.");
                }

                throw new Exception("Something went wrong, item has not been deleted.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(TaskController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }


        private async Task<User> GetCurrentUser()
        {
            try
            {
                //get identity userid from claims
                var userId = User.Claims.First(c => c.Type == "UserID").Value;

                //get identity class
                var identityUser = await _userManager.FindByIdAsync(userId);

                //get user class by identity username
                var user = await _userService.GetByUserEmailAsync(identityUser.Email);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error GetCurrentUser(): {e.Message}");
                return null;
            }
        }

    }
}
