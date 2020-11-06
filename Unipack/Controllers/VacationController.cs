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

        public VacationController(UserManager<IdentityUser> userManager, IVacationService vacationService, ILogger<ItemController> logger)
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
        /// Finds an Item with the specified id.
        /// </summary>
        /// <param name="itemId">The id of the Item you're looking to get.</param>  
        [HttpGet("{vacationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VacationDto> GetVacation(int vacationId)
        {
            try
            {
                var result = _vacationService.GetVacationById(vacationId);
                return Ok(result);

            }
            catch (ItemNotFoundException e)
            {
                return NotFound(new { message = "Not found: " + e.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }
        }
    }
       
}
