using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly IVacationService _vacationService;
        private readonly IUserService _userService;

        public VacationController(UserManager<IdentityUser> userManager, IVacationService packListService, IUserService userService, ILogger<VacationController> logger)
        {
            _userManager = userManager;
            _vacationService = packListService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a Vacation.
        /// </summary>
        /// <param name="model">This is the Vacation model with the required information.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult> AddVacation([FromBody] VacationDto model)
        {
            var user = await GetCurrentUser();
            var packList = new PackList(model.Name, await GetCurrentUser());

            if (_vacationService.AddVacation(model, user))
                return Ok();
            else
                throw new Exception("Something went wrong, vacation has not been added.");
        }
        /// <summary>
        /// Returns all Vacations created by the authenticated user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<List<VacationDto>>> GetAllVacations()
        {
            var user = await GetCurrentUser();

            var result = _vacationService.GetAllVacationsByUser(user.UserId);
            if (result != null)
                return new OkObjectResult(result.Select(x => new VacationDto
                {
                    Name = x.Name,
                    VacationId = x.VacationId,
                    AddedOn = x.AddedOn,
                    DateDeparture = x.DateDeparture,
                    DateReturn = x.DateReturn,
                    Locations = x.Locations.Select(loc => new VacationLocationDto
                    {
                        CityName = loc.CityName,
                        DateDeparture = loc.DateDeparture,
                        VacationLocationId = loc.VacationLocationId,
                        AddedOn = loc.AddedOn,
                        CountryName = loc.CountryName,
                        DateArrival = loc.DateArrival,
                    }).ToList()
                }).ToList());
            return NotFound();
        }

        /// <summary>
        /// Update a Vacation with the specified id.
        /// </summary>
        /// <param name="vacationId">The id of the PackList you're looking to update.</param>
        /// <param name="model">This is the PackList model with the required information.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{vacationId}")]
        public async  Task<ActionResult> UpdateVacation(int vacationId, [FromBody] VacationDto model)
        {
            var user = await GetCurrentUser();

            try
            {
                var vac = _vacationService.GetVacationById(vacationId);
                if (vac.Author.UserId == user.UserId)
                {
                    return new OkObjectResult(_vacationService.UpdateVacation(vacationId, model));
                }
                else
                    throw new VacationNotFoundException(vacationId);
            }
            catch (VacationNotFoundException e)
            {
                return BadRequest(new { message = "Error while finding vacation: " + e.Message });
            }
        }

        /// <summary>
        /// Delete a Vacation with the specified id.
        /// </summary>
        /// <param name="vacationId">The id of the PackList you're looking to delete.</param>
        [HttpDelete("{vacationId}")]
        public async Task<ActionResult> DeleteVacation(int vacationId)
        {
            var user = await GetCurrentUser();

            try
            {
                var vac = _vacationService.GetVacationById(vacationId);
                if (vac.Author.UserId == user.UserId)
                {
                    return new OkObjectResult(_vacationService.DeleteVacationById(vacationId));
                }
                else
                    throw new VacationNotFoundException(vacationId);
            }catch(VacationNotFoundException e)
            {
                return BadRequest(new { message = "Error while finding vacation: " + e.Message });
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
