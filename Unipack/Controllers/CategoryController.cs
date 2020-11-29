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
    [Route("unipack/api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public CategoryController
        (
            IUserService userService,
            UserManager<IdentityUser> userManager,
            ILogger<ItemController> logger,
            ICategoryService categoryService
        )
        {
            this._logger = logger;
            this._userService = userService;
            this._userManager = userManager;
            this._categoryService = categoryService;
        }

        /// <summary>
        /// Returns all Categories created by the authenticated user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            try
            {

                var user = await GetCurrentUser();
                var result = _categoryService.GetAllCategoriesByUserId(user.UserId)
                    .Select(x => new CategoryDto
                    {
                         CategoryId = x.CategoryId,
                         Name = x.Name,
                         AddedOn = x.AddedOn,
                         Author = new UserDto
                         {
                             Email = x.Author.Email,
                             Username = x.Author.Username,
                             FirstName = x.Author.Firstname,
                             LastName = x.Author.Firstname,
                             UserId = x.Author.UserId
                         }
                    }
                    ).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(CategoryController)}/{methodName}: {e.Message}");
                return BadRequest(new { message = "Internal server error: " + e.Message });
            }

        }

        /// <summary>
        /// Finds a Category with the specified id.
        /// </summary>
        /// <param name="categoryId">The id of the Category you're looking to get.</param>  
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int categoryId)
        {
            try
            {
                var user = await GetCurrentUser();
                var category = _categoryService.GetCategoryById(categoryId);
                if (category.Author.UserId == user.UserId)
                {
                    var result = new CategoryDto
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        AddedOn = category.AddedOn,
                        Author = new UserDto
                        {
                            Email = category.Author.Email,
                            Username = category.Author.Username,
                            FirstName = category.Author.Firstname,
                            LastName = category.Author.Firstname,
                            UserId = category.Author.UserId
                        }
                    };
                    return Ok(result);
                }
                return BadRequest("This category does not belong to your account");
            }
            catch (CategoryNotFoundException e)
            {
                return NotFound(new { message = "Category not found: " + e.Message });
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(CategoryController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a Category with the passed on Category DTO model.
        /// </summary>
        /// <param name="categoryDto">The Category DTO model of the Category to be created.</param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var user = await GetCurrentUser();
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Author = user,
                };
                if (_categoryService.AddCategory(category))
                    return Ok(true);
                throw new Exception("Something went wrong, category has not been created.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(CategoryController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a Category with the passed on Category DTO Model.
        /// </summary>
        /// <param name="categoryId">The Id of the Category to be updated.</param>  
        /// <param name="categoryDto">The Category DTO Model of the Category to be updated.</param>  
        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                var user = await GetCurrentUser();
                var update_cat = new Category
                {
                    Name = categoryDto.Name,
                };
                if (_categoryService.UpdateCategory(categoryId, update_cat))
                    return Ok(true);
                throw new Exception("Something went wrong, category has not been updated.");
            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(CategoryController)}/{methodName}: {e.Message}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Deletes a Category with the specified id.
        /// </summary>
        /// <param name="categoryId">The Id of the Category to be updated.</param>  
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteCategory(int categoryId)
        {
            try
            {
                var user = await GetCurrentUser();
                if (_categoryService.GetCategoryById(categoryId).Author.UserId == user.UserId)
                {
                    if (_categoryService.DeleteCategoryById(categoryId))
                        return Ok(true);
                    throw new Exception("Something went wrong, category has not been deleted.");
                }
                return BadRequest("This category does not belong to your account.");

            }
            catch (Exception e)
            {
                var methodName = this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                _logger.LogError($"[INTERNAL ERROR] Something broke in {nameof(CategoryController)}/{methodName}: {e.Message}");
                return StatusCode(500);
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
