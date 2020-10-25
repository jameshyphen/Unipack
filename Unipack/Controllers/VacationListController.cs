using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VacationListController : ControllerBase
    {
        /// <summary>
        /// Returns all the VacationLists the authorized user has created
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IEnumerable<string> Get()
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
        public void Post([FromBody] string vacationListDto)
        {
            throw new NotImplementedException();
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
        public void Put(int id, [FromBody] string vacationListDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
