using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Unipack.Enums;
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
            await CreateUser("uwptest", "uwp.test@gmail.com", password);
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
            var uwptest = new User("uwp", "test", "uwp.test@gmail.com");
            var luna = new User("Luna", "dv", "luna.devuyst@student.hogent.be");
            var tijl = new User("Tijl", "Zwartjes", "tijl@hotmail.com");
            var users = _context.UnipackUsers;
            uwptest.Username = "uwptest";
            luna.Username = "lunadv";
            tijl.Username = "tijlzwartjes";
            luna.Username = "nicklersberghe";
            users.Add(uwptest);
            users.Add(luna);
            users.Add(tijl);
            users.Add(new User("John", "Cena", "john@ce.na"));
            users.Add(new User("Billie", "Eilish", "liketheothergirls@unoriginal.com"));
            users.Add(new User("Joseph", "Stalin", "gulag@comrade.gulu"));
            users.Add(new User("Napoleon", "Bonaparte", "short@men.riseup"));
            users.Add(new User("Post", "Malone", "water@melo.ne"));
            users.Add(new User("Lil", "Pump", "plum6969@esket.it"));

            // Create a vacation
            Vacation ronaVac = 
                new Vacation(
                    "Tour de la Corona",
                    uwptest,
                    new DateTime(2021, 3, 20),
                    new DateTime(2021, 4, 20)
                    );

            // Add some lists to the vacation
            PackList ronaVacList = 
                new PackList(
                        "Important stuff, DONT FORGET!", uwptest
                    );
            PackList ronaVacList2 =
                new PackList(
                    "Optional stuff!", uwptest
                );
            VacationLocation vacLoc = new VacationLocation
            {
                CityName = "Barcelona",
                CountryName = "Spain",
                DateArrival = new DateTime(2021, 2, 20),
                DateDeparture = new DateTime(2021, 2, 22)
            };

            ronaVac.Locations.Add(vacLoc);
            // Add some item categories
            Category ronaCategory = new Category("Technology", uwptest);

            Category ronaCategory2 = new Category("Books", uwptest);


            // Add some items to the lists
            Item ronaItem = new Item("Laptop", uwptest){Category = ronaCategory};
            Item ronaItem2 = new Item("Phone", uwptest) {Category = ronaCategory};

            Item ronaItem3 = new Item("Clean Coder book", uwptest) { Category = ronaCategory2 };

            // Add some tasks to the lists

            PackTask task1 = new PackTask("Charge up my phone", uwptest, DateTime.Parse("12/12/2020"));

            task1.Priority = Priority.High;
            task1.Completed = true;
            ronaVacList.Tasks.Add(task1);
            ronaVacList.Items.Add(new PackItem(ronaVacList, ronaItem) { Quantity = 1 });
            ronaVacList.Items.Add(new PackItem(ronaVacList, ronaItem2) { Quantity = 2 });
            ronaVacList.Items.Add(new PackItem(ronaVacList, ronaItem3) { Quantity = 1 });
            // Add some locations to the lists

            ronaVac.AddList(ronaVacList);
            ronaVac.AddList(ronaVacList2);
            uwptest.AddVacation(ronaVac);
            _context.SaveChanges();
        }
    }
}
