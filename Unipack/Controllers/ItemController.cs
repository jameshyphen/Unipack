using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unipack.Data.Interfaces;
using Unipack.DTOs;


namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService service;
        public ItemController(IItemService service)
        {
            this.service = service;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Task<IEnumerable<ItemDto>>> GetAllItems()
        {
            var user = User.Identity.Name;
            //TODO implement method to get userid
            return service.GetAllItemsByUser(0);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Task<ItemDto>> GetItem(int id)
        {
            return service.GetItemById(id);
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Task<IEnumerable<string>>> GetAllCategories()
        {
            var user = User.Identity.Name;
            //TODO implement method to get userid
            return service.GetAllCategoriesByUser(0);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> AddCategory([FromBody] string value)
        {
            return service.AddCategory(value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> AddItemToCategory(int id,[FromBody] string categoryName)
        {
            return service.AddItemToCategory(id, categoryName);
        }
        

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> DeleteCategory(string name)
        {
            return service.DeleteCategoryByName(name);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> AddItem([FromBody] ItemDto value)
        {
            return service.AddItem(value);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> UpdateItem(int id, [FromBody] ItemDto value)
        {
            return service.UpdateItem(id, value);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<bool> DeleteItem(int id)
        {
            return service.DeleteItemById(id);
        }
    }
}
