using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;
using Unipack.Data;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;
using Xunit;



namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IVacationService _vacationService;
        private readonly ILogger _logger;

        public VacationController(UserManager<IdentityUser> userManager, IVacationService vacationService, ILogger<VacationController> logger)
        {
            _userManager = userManager;
            _vacationService = vacationService;
            _logger = logger;
        }

        /// <summary>
        /// Returns all Vacations created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ICollection<VacationDto>>> GetAllVacationsFromUser()
        {
            try
            {
                User user = (User)User.Identity;
                var result = _vacationService.GetAllVacationsByUserId(user.UserId);
                return Ok(result);
            }
            catch (UserNotFoundException ue)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {ue.Message}");
                return BadRequest(new { message = "Could not find the user: " + ue.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }
        }

        /// <summary>
        /// Finds a Vacation with the specified id.
        /// </summary>
        /// <param name="vacationId">The id of the Vacation you're looking to get.</param>  
        [HttpGet("{vacationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Vacation> GetVacation(int vacationId)
        {
            try
            {
                var result = _vacationService.GetVacationById(vacationId);
                return Ok(result);

            }
            catch (VacationNotFoundException ve)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {ue.Message}");
                return BadRequest(new { message = "Could not find the user: " + ve.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }
        }

        
        /// <summary>
        /// Creates an Vacation with the passed on Vacation DTO model.
        /// </summary>
        /// <param name="vacationDto">The Vacation DTO model of the Vacation to be created.</param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddVacation([FromBody] VacationDto vacationDto)
        {
            try
            {
                Vacation vacation = new Vacation
                {
                    Name = vacationDto.Name,
                    DateReturn = vacationDto.DateReturn,
                    DateDeparture = vacationDto.DateDeparture
                };
                if (_vacationService.AddVacation(vacation))
                    return Ok(true);
                throw new Exception("Something went wrong, vacation has not been created.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a Vacation with the passed on Vacation DTO Model.
        /// </summary>
        /// <param name="vacationId">The Id of the Vacation to be updated.</param>  
        /// <param name="vacationDto">The Vacation DTO Model of the Vacation to be updated.</param>  
        [HttpPut("{vacationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateVacation(int vacationId, [FromBody] VacationDto vacationDto)
        {

            try
            {
                Vacation vacation = new Vacation
                {
                    Name = vacationDto.Name,
                    DateReturn = vacationDto.DateReturn,
                    DateDeparture = vacationDto.DateDeparture
                };
                if (_vacationService.UpdateVacation(vacationId, vacation))
                    return Ok(true);
                throw new Exception("Something went wrong, vacation has not been updated.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a Vacation with the specified id.
        /// </summary>
        /// <param name="vacationId">The Id of the Vacation to be updated.</param>  
        [HttpDelete("{vacationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteVacation(int vacationId)
        {
            try
            {
                return _vacationService.DeleteVacation(vacationId);
            }
            catch (VacationNotFoundException ve)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {ve.Message}");
                return BadRequest(new { message = "Could not find the vacation: " + ve.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(VacationController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }
    }
       
}
