using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unipack.DTOs
{
    public class UserDto
    {
        /* "userId": 2,
        "email": null,
        "userName": "nicklersberghe",
        "firstname": "Nick",
        "lastname": "Lersberghe",
         */
        public int UserId { get; set; }
        public string Email { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public UserDto(int userId, string email, string userName, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            Username = userName;
            FirstName = firstName;
            LastName = lastName;
        }
        public UserDto() { }
    }
}
