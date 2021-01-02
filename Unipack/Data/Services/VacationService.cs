using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class VacationService : IVacationService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<Vacation> _vacations;
        private readonly DbSet<User> _users;

        public VacationService(Context context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _vacations = context.Vacations;
            _users = context.UnipackUsers;
        }

        public bool AddVacation(VacationDto vacationDto)
        {
            var vacation = new Vacation(vacationDto.Name, vacationDto.DateDeparture, vacationDto.DateReturn);

            _vacations.Add(vacation);

            return _context.SaveChanges() != 0;
        }

        public bool DeleteVacationById(int id)
        {
            var vacation = _vacations.FirstOrDefault(x => x.VacationId == id) ?? throw new VacationNotFoundException(id);

            _vacations.Remove(vacation);

            return _context.SaveChanges() != 0;
        }

        public ICollection<Vacation> GetAllVacationsByUser(int userId)
        {
            User user = _users.Include(x => x.Vacations).FirstOrDefault(u => u.UserId == userId) ?? throw new UserNotFoundException(userId);

            return user.Vacations.OrderByDescending(l => l.AddedOn).ToList();
        }

        public Vacation GetVacationById(int id)
        {
            var vacation = _vacations.FirstOrDefault(x => x.VacationId == id) ?? throw new VacationNotFoundException(id);

            return vacation;
        }

        public bool UpdateVacation(int id, VacationDto model)
        {
            var vacation = _vacations.FirstOrDefault(l => l.VacationId == id) ?? throw new VacationNotFoundException(id);

            vacation.DateReturn = model.DateReturn;
            vacation.DateDeparture = model.DateDeparture;
            vacation.Name = model.Name;

            _vacations.Update(vacation);
            return _context.SaveChanges() != 0;
        }
    }
}
