using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unipack.Data.Interfaces;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<User> _users;

        public UserService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._users = context.Users;
        }

        public void Add(User user)
        {
            _users.Add(user);

            _context.SaveChanges();
        }
        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _users.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }

        public User GetByUserName(string username)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(username)) ?? throw new ArgumentException("Something went wrong finding user.");
        }

        public bool UsernameAvailable(string username)
        {
            var user = _users.FirstOrDefault(x => x.Username.Equals(username));
            return user == null;
        }

        public ICollection<User> GetAll()
        {
            return this._users.ToList();
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(b => b.UserId.Equals(id));
        }

        public User GetByMail(string email)
        {
            return _users.FirstOrDefault(b => b.Email.Equals(email));
        }
    }
}
