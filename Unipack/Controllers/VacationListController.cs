using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipack.Data;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
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
        /// Returns all the VacationLists the authorized user has created
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<VacationListDto> GetAll(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a specific list by id.
        /// </summary>
        /// <param name="id"></param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="vacationListDto"></param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public bool Post([FromBody] VacationListDto vacationListDto)
        {
            return _vacationListService.AddVacationList();
        }
        /// <summary>
        /// Update a list.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacationListDto"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] VacationListDto vacationListDto)
        {
            return _vacationListService.AddItemToListByItemId(id,vacationListDto.VacationListId);
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
             return _vacationListService.DeleteVacationListById(id);
        }
    }
}
