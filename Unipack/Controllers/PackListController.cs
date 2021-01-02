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
    public class PackListController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPackListService _packListService;
        private readonly IUserService _userService;

        public PackListController(UserManager<IdentityUser> userManager, IPackListService packListService, IUserService userService)
        {
            this._userManager = userManager;
            this._packListService = packListService;
            this._userService = userService;
        }

        /// <summary>
        /// Returns all PackLists created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetPackListFromUser(User user)
        {
            var result = _packListService.GetAllPackListsByUser(user.UserId);
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        /// <summary>
        /// Finds a PackList with the specified id.
        /// </summary>
        /// <param name="id">The id of the PackList you're looking to get.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> GetPackList(int id)
        {
            var result = new object();
            try
            {
                
                result = await _packListService.GetPackListById(id);
            }
            catch (PackListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding pack list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Creates a PackList.
        /// </summary>
        /// <param name="model">This is the PackListDto model with the required information.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult AddPackList([FromBody] PackListDto model)
        {
                if (_packListService.AddPackList(model))
                    return Ok();
                else
                    throw new Exception("Something went wrong, pack list has not been added.");
        }
        /// <summary>
        /// Update a PackList with the specified id.
        /// </summary>
        /// <param name="id">The id of the PackList you're looking to update.</param>
        /// <param name="model">This is the PackList model with the required information.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult UpdatePackList(int id, [FromBody] PackListDto model)
        {
            bool result;
            try
            {

                result = _packListService.UpdatePackList(id, model);
            }
            catch (PackListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding pack list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Add an Item to PackList.
        /// </summary>
        /// <param name="packListId">The id of the PackList you're looking to update.</param>
        /// <param name="itemId">This is the PackList model with the required information.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult AddItemToPackList(int packListId, int itemId)
        {
            bool result;
            try
            {

                result = _packListService.UpdatePackList(id, model);
            }
            catch (PackListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding pack list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Deletes a PackList with the specified id.
        /// </summary>
        /// <param name="id">The id of the PackList you're looking to delete.</param>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult DeletePackList(int id)
        {
            
            bool result;
            try
            {
                result = _packListService.DeletePackListById(id);
            }
            catch (PackListNotFoundException ve)
            {
                return BadRequest(new { message = "Error while finding pack list: " + ve.Message });
            }
            return new OkObjectResult(result);
        }
    }
}
