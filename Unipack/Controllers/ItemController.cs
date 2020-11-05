﻿using System;
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
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public ItemController(IUserService userService, UserManager<IdentityUser> userManager, ILogger<ItemController> logger, IItemService itemService)
        {
            this._logger = logger;
            this._userService = userService;
            this._userManager = userManager;
            this._itemService = itemService;
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
