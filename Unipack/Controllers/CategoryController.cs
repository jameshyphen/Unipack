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
using Unipack.Exceptions;
using Unipack.Models;


namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public CategoryController
        (
            IUserService userService,
            UserManager<IdentityUser> userManager,
            ILogger<ItemController> logger,
            IItemService itemService,
            ICategoryService categoryService
        )
        {
            this._logger = logger;
            this._userService = userService;
            this._userManager = userManager;
            this._itemService = itemService;
            this._categoryService = categoryService;
        }

        /// <summary>
        /// Returns all Items created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ICollection<ItemDto>>> GetAllItems()
        {
            User user = await GetCurrentUser();
            List<ItemDto> result = _itemService.GetAllItemsByUserId(user.UserId)
                .Select(x => new ItemDto
                    {
                        ItemId = x.ItemId,
                        AddedOn = x.AddedOn,
                        Name = x.AddedOn.ToString("d"),
                        Category = x.Category.Name
                    }
                ).ToList();
            return Ok(result);
        }

        /// <summary>
        /// Finds an Item with the specified id.
        /// </summary>
        /// <param name="itemId">The id of the Item you're looking to get.</param>  
        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ItemDto> GetItem(int itemId)
        {
            try
            {
                Item item = _itemService.GetItemById(itemId);
                ItemDto result = new ItemDto
                {
                    ItemId = item.ItemId,
                    AddedOn = item.AddedOn,
                    Name = item.AddedOn.ToString("d"),
                    Category = item.Category.Name
                };
                return Ok(result);

            }
            catch (ItemNotFoundException e)
            {
                return NotFound(new {message = "Not found: " + e.Message});
            }
            catch (Exception e)
            {
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}: {e.Message}");
                return BadRequest(new {message = "Internal server error: " + e.Message});
            }
        }


        // TODO: You cannot have 2 gets in the same controller, causes conflict lol
        // TODO: Create a categorycontroller...
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public ActionResult<Task<IEnumerable<string>>> GetAllCategories()
        //{
        //    var user = User.Identity.Name;
        //    //TODO implement method to get userid
        //    return service.GetAllCategoriesByUser(0);
        //}

        // TODO: Same issue, move to category controller
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public ActionResult<bool> AddCategory([FromBody] string value)
        //{
        //    return service.AddCategory(value);
        //}

        // TODO: All of these endpoints are not going to work, they are all on the same api/Item/{id} http get, and the above ones are all on api/Item,
        // TODO: please make unique routes and correct request types
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public ActionResult<bool> AddItemToCategory(int id,[FromBody] string categoryName)
        //{
        //    return service.AddItemToCategory(id, categoryName);
        //}

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public ActionResult<bool> DeleteCategory(string name)
        //{
        //    return service.DeleteCategoryByName(name);
        //}

        /// <summary>
        /// Creates an Item.
        /// </summary>
        /// <param name="model">This is the ItemDto model with the required information.</param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> AddItem([FromBody] ItemDto model)
        {
            return _itemService.AddItem(model);
        }

        /// <summary>
        /// Updates an Item with the specified id.
        /// </summary>
        /// <param name="id">The id of the Item you're looking to update.</param>  
        /// <param name="model">This is the ItemDto model with the required information.</param>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> UpdateItem(int id, [FromBody] ItemDto model)
        {
            return _itemService.UpdateItem(id, model);
        }

        /// <summary>
        /// Deletes an Item with the specified id.
        /// </summary>
        /// <param name="id">The id of the Item you're looking to delete.</param>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> DeleteItem(int id)
        {
            return _itemService.DeleteItemById(id);
        }

        private async Task<User> GetCurrentUser()
        {
            try
            {
                //get identity userid from claims
                string userId = User.Claims.First(c => c.Type == "UserID").Value;

                //get identity class
                var identityUser = await _userManager.FindByIdAsync(userId);

                //get user class by identity username
                User user = await _userService.GetByUserEmailAsync(identityUser.Email);
                return user;
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation($"Error GetCurrentUser(): {e.Message}");
            }

            return null;
        }
    }
}
