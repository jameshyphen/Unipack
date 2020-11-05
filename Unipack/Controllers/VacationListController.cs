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
using Unipack.Models;
using Xunit;

namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VacationListController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IVacationListService _vacationListService;
        private readonly IUserService _userService;

        public VacationListController(UserManager<IdentityUser> userManager, IVacationListService vacationListService, IUserService userService)
        {
            this._userManager = userManager;
            this._vacationListService = vacationListService;
            this._userService = userService;
        }

        /// <summary>
        /// Returns all VacationLists created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetVacationListFromUser(User user)
        {
            var result = _vacationListService.GetAllVacationListsByUser(user.UserId);
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        /// <summary>
        /// Finds a VacationList with the specified id.
        /// </summary>
        /// <param name="id">The id of the VacationList you're looking to get.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> GetVacationList(int id)
        {
            var result = new object();
            try
            {
                
                result = await _vacationListService.GetVacationListById(id);
            }
            catch (VacationListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding vacation list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Creates a VacationList.
        /// </summary>
        /// <param name="model">This is the VacationListDto model with the required information.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult AddVacationList([FromBody] VacationListDto model)
        {
                if (_vacationListService.AddVacationList(model))
                    return Ok();
                else
                    throw new Exception("Something went wrong, vacationList has not been added.");
        }
        /// <summary>
        /// Update a VacationList with the specified id.
        /// </summary>
        /// <param name="id">The id of the VacationList you're looking to update.</param>
        /// <param name="model">This is the VacationList model with the required information.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult UpdateVacationList(int id, [FromBody] VacationListDto model)
        {
            bool result;
            try
            {

                result = _vacationListService.UpdateList(id, model);
            }
            catch (VacationListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding vacation list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Deletes a VacationList with the specified id.
        /// </summary>
        /// <param name="id">The id of the VacationList you're looking to delete.</param>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            
            bool result;
            try
            {
                result = _vacationListService.DeleteVacationListById(id);
            }
            catch (VacationListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding vacation list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }
    }
}
