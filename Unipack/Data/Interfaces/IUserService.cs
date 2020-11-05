using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface IUserService
    {
        void Add(User user);
        User GetById(int id);
        ICollection<User> GetAll();
        Task<User> GetByUserNameAsync(string username);
        Task<User> GetByUserEmailAsync(string username);
        User GetByUserName(string username);
        bool UsernameAvailable(string username);
    }
}
