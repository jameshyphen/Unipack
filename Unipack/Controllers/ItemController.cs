using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
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
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public ItemController(IUserService userService, UserManager<IdentityUser> userManager, ILogger<ItemController> logger, IItemService itemService, ICategoryService categoryService)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
            _itemService = itemService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Returns all Items created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ItemDto>>> GetAllItems()
        {
            try
            {
                var user = await GetCurrentUser();
                var result = _itemService.GetAllItemsByUserId(user.UserId)
                    .Select(x => new ItemDto
                        {
                            ItemId = x.ItemId,
                            Name = x.Name,
                            AddedOn = x.AddedOn,
                            CategoryId = x.Category?.CategoryId ,
                            CategoryName = x.Category?.Name
                        }
                    ).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }
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
                var item = _itemService.GetItemById(itemId);
                var result = new ItemDto
                {
                    ItemId = item.ItemId,
                    AddedOn = item.AddedOn,
                    Name = item.AddedOn.ToString("d"),
                    CategoryName = item.Category.Name
                };
                return Ok(result);

            }
            catch (ItemNotFoundException e)
            {
                return NotFound(new {message = "Not found: " + e.Message});
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates an Item with the passed on Item DTO model.
        /// </summary>
        /// <param name="itemDto">The Item DTO model of the Item to be created.</param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddItem([FromBody] ItemDto itemDto)
        {
            try
            {
                var user = await GetCurrentUser();
                var category = itemDto.CategoryId != null ? _categoryService.GetCategoryById((int) itemDto.CategoryId) : null;
                var item = new Item
                {
                    Name = itemDto.Name,
                    Author = user,
                    Category = category,
                    Priority = itemDto.Priority
                };
                if (_itemService.AddItem(item))
                    return Ok(true);
                throw new Exception("Something went wrong, item has not been created.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates an Item with the passed on Item DTO Model.
        /// </summary>
        /// <param name="itemId">The Id of the Item to be updated.</param>  
        /// <param name="itemDto">The Item DTO Model of the Item to be updated.</param>  
        [HttpPut("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateItem(int itemId, [FromBody] ItemDto itemDto)
        {
            try
            {
                var user = await GetCurrentUser();
                var category = itemDto.CategoryId != null ?_categoryService.GetCategoryById((int) itemDto.CategoryId) : null;
                var item = new Item
                {
                    Name = itemDto.Name,
                    Author = user,
                    Category = category,
                    Priority = itemDto.Priority
                };
                if (_itemService.UpdateItem(itemId, item))
                    return Ok(true);
                throw new Exception("Something went wrong, item has not been updated.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an Item with the specified id.
        /// </summary>
        /// <param name="itemId">The Id of the Item to be updated.</param>  
        [HttpDelete("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteItem(int itemId)
        {
            try
            {
                var user = await GetCurrentUser();
                if (_itemService.GetItemById(itemId).Author.UserId == user.UserId)
                {
                    if (_itemService.DeleteItemById(itemId))
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
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(ItemController)}/{methodName}: {e.Message}");
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
