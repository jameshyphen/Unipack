using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Unipack.Models;

namespace Unipack.Data
{
    public class DataInit
    {

        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DataInit> _logger;

        public DataInit(Context context, UserManager<IdentityUser> userManager, ILogger<DataInit> logger)
        {
            this._context = context;
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task InitAsync()
        {
            //recreate DB under
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                InitData();
                await InitializeUsers();
            }
            else
            {
                throw new Exception("Database Unipack could not be created");
            }
        }

        private async Task InitializeUsers()
        {
            const string password = "password";
            await CreateUser("dzhemaptula", "dzhem.aptula@gmail.com", password);
            await CreateUser("tijlzwartjes", "tijl@gmail.com", password);
            await CreateUser("lunadv", "luna.devuyst@student.hogent.be", password);
            await CreateUser("josephstalin", "joseph@stal.in", password);
            await CreateUser("johncena", "john@ce.na", password);
            await CreateUser("web4", "student@hogent.be", password);
        }

        private async Task CreateUser(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            try
            {
                await _userManager.CreateAsync(user, password);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error from datainit: {e.Message}");
            }

        }

        private void InitData()
        {
            var dzhem = new User("Dzhem", "Aptula", "dzhem.aptula@gmail.com");
            var nick = new User("Nick", "Lersberghe", "nick@gmail.com");
            var tijl = new User("Tijl", "Zwartjes", "tijl@hotmail.com");
            var users = _context.Users;
            dzhem.Username = "dzhemaptula";
            nick.Username = "nicklersberghe";
            users.Add(dzhem);
            users.Add(nick);
            users.Add(new User("Janne", "Vschep", "jannev@gmail.com"));
            users.Add(tijl);
            users.Add(new User("John", "Cena", "cantcme@wwe.org"));
            users.Add(new User("Billie", "Eilish", "liketheothergirls@unoriginal.com"));
            users.Add(new User("Joseph", "Stalin", "gulag@comrade.gulu"));
            users.Add(new User("Napoleon", "Bonaparte", "short@men.riseup"));
            users.Add(new User("Post", "Malone", "water@melo.ne"));
            users.Add(new User("Lil", "Pump", "plum6969@esket.it"));
            var web4 = new User("student", "hogent", "student@hogent.be", "web4");
            users.Add(web4);

            _context.SaveChanges();
        }
    }
}
