//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Unipack.Data.Interfaces;
//using Unipack.DTOs;


//namespace Unipack.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ItemController : ControllerBase
//    {
//        private readonly IItemService _itemService;
//        public ItemController(IItemService itemService)
//        {
//            this._itemService = itemService;
//        }

//        /// <summary>
//        /// Returns all Items created by the authenticated user.
//        /// </summary>
//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        public ActionResult<Task<IEnumerable<ItemDto>>> GetAllItems()
//        {
//            var user = User.Identity.Name;
//            //TODO implement method to get userid
//            return _itemService.GetAllItemsByUser(0);
//        }

//        /// <summary>
//        /// Finds an Item with the specified id.
//        /// </summary>
//        /// <param name="id">The id of the Item you're looking to get.</param>  
//        [HttpGet("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        public ActionResult<Task<ItemDto>> GetItem(int id)
//        {
//            return _itemService.GetItemById(id);
//        }


//        // TODO: You cannot have 2 gets in the same controller, causes conflict lol
//        // TODO: Create a categorycontroller...
//        //[HttpGet]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        //public ActionResult<Task<IEnumerable<string>>> GetAllCategories()
//        //{
//        //    var user = User.Identity.Name;
//        //    //TODO implement method to get userid
//        //    return service.GetAllCategoriesByUser(0);
//        //}

//        // TODO: Same issue, move to category controller
//        //[HttpPost]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        //public ActionResult<bool> AddCategory([FromBody] string value)
//        //{
//        //    return service.AddCategory(value);
//        //}

//        // TODO: All of these endpoints are not going to work, they are all on the same api/Item/{id} http get, and the above ones are all on api/Item,
//        // TODO: please make unique routes and correct request types
//        //[HttpGet("{id}")]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        //public ActionResult<bool> AddItemToCategory(int id,[FromBody] string categoryName)
//        //{
//        //    return service.AddItemToCategory(id, categoryName);
//        //}

//        //[HttpDelete]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        //public ActionResult<bool> DeleteCategory(string name)
//        //{
//        //    return service.DeleteCategoryByName(name);
//        //}

//        /// <summary>
//        /// Creates an Item.
//        /// </summary>
//        /// <param name="model">This is the ItemDto model with the required information.</param>  
//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        public ActionResult<bool> AddItem([FromBody] ItemDto model)
//        {
//            return _itemService.AddItem(model);
//        }

//        /// <summary>
//        /// Updates an Item with the specified id.
//        /// </summary>
//        /// <param name="id">The id of the Item you're looking to update.</param>  
//        /// <param name="model">This is the ItemDto model with the required information.</param>  
//        [HttpPut("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        public ActionResult<bool> UpdateItem(int id, [FromBody] ItemDto model)
//        {
//            return _itemService.UpdateItem(id, model);
//        }

//        /// <summary>
//        /// Deletes an Item with the specified id.
//        /// </summary>
//        /// <param name="id">The id of the Item you're looking to delete.</param>  
//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        public ActionResult<bool> DeleteItem(int id)
//        {
//            return _itemService.DeleteItemById(id);
//        }
//    }
//}
