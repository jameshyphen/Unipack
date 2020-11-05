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
        /// Returns all VacationLists created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<VacationListDto> GetVacationLists(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds a VacationList with the specified id.
        /// </summary>
        /// <param name="id">The id of the VacationList you're looking to get.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}", Name = "Get")]
        public string GetVacationList(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a VacationList.
        /// </summary>
        /// <param name="model">This is the VacationListDto model with the required information.</param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public bool CreateVacationList([FromBody] VacationListDto model)
        {
            throw new NotImplementedException();
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
        public bool UpdateVacationList(int id, [FromBody] VacationListDto model)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a VacationList with the specified id.
        /// </summary>
        /// <param name="id">The id of the VacationList you're looking to delete.</param>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
