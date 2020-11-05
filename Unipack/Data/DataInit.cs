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
                InitUsers();
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

        private void InitUsers()
        {
            var dzhem = new User("Dzhem", "Aptula", "dzhem.aptula@gmail.com");
            var luna = new User("Luna", "dv", "luna.devuyst@student.hogent.be");
            var tijl = new User("Tijl", "Zwartjes", "tijl@hotmail.com");
            var users = _context.UnipackUsers;
            dzhem.Username = "dzhemaptula";
            luna.Username = "nicklersberghe";
            users.Add(dzhem);
            users.Add(luna);
            users.Add(tijl);
            users.Add(new User("John", "Cena", "john@ce.na"));
            users.Add(new User("Billie", "Eilish", "liketheothergirls@unoriginal.com"));
            users.Add(new User("Joseph", "Stalin", "gulag@comrade.gulu"));
            users.Add(new User("Napoleon", "Bonaparte", "short@men.riseup"));
            users.Add(new User("Post", "Malone", "water@melo.ne"));
            users.Add(new User("Lil", "Pump", "plum6969@esket.it"));
            var web4 = new User("student", "hogent", "student@hogent.be", "web4");
            users.Add(web4);

            // Create a vacation
            Vacation ronaVac = 
                new Vacation(
                    "Tour de la Corona",
                    new DateTime(2021, 3, 20),
                    new DateTime(2021, 4, 20)
                    );

            // Add some lists to the vacation
            VacationList ronaVacList = 
                new VacationList(
                        "Important stuff, DONT FORGET!"
                    );
            VacationList ronaVacList2 =
                new VacationList(
                    "Optional stuff!"
                );

            // Add some item categories
            Category ronaCategory = new Category("Technology", dzhem);

            Category ronaCategory2 = new Category("Books", dzhem);


            // Add some items to the lists
            Item ronaItem = new Item("Laptop"){Category = ronaCategory};
            Item ronaItem2 = new Item("Phone"){Category = ronaCategory};

            Item ronaItem3 = new Item("Clean Coder book") { Category = ronaCategory2 };

            // Add some tasks to the lists



            // Add some locations to the lists

            ronaVac.AddList(ronaVacList);
            ronaVac.AddList(ronaVacList2);
            dzhem.AddVacation(ronaVac);
            _context.SaveChanges();
        }
    }
}
