using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipack.Data.Interfaces;

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
        /// Returns all Vacations created by the authenticated user
        /// </summary>
        [HttpGet]
        public IEnumerable<string> GetAllVacations()
        {
            var result = _vacationService.GetAllPackLists(user.UserId);
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        /// <summary>
        /// TEST 2.
        /// </summary>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// TEST 2.
        /// </summary>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// TEST 2.
        /// </summary>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Returns all Items created by the authenticated user.
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
