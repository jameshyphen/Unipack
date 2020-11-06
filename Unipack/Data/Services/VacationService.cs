using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions.NotFoundExceptions;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class VacationService : IVacationService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<VacationList> _vacationLists;
        private readonly DbSet<Vacation> _vacations;
        private readonly DbSet<User> _users;

        public VacationService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._vacationLists = context.VacationLists;
            this._vacations = context.Vacations;
            this._users = context.UnipackUsers;
        }

       
        /// <summary>
        /// Add a Vacation to the database. Returns false if 0 changes are made
        /// </summary>
        /// <param name="list"></param>
        /// <returns>boolean of any made changes</returns>
        public bool AddVacation(Vacation vacation)
        {
            _vacations.Add(new Vacation() { Name = vacation.Name, DateDeparture = vacation.DateDeparture, DateReturn = vacation.DateReturn });
            return _context.SaveChanges() != 0;
        }

        public bool DeleteVacation(int vacationId)
        {
            var vac = _vacations.Where(v => v.VacationId == vacationId).FirstOrDefault() ?? throw new VacationNotFoundException(vacationId);
            _vacations.Remove(vac);
            return _context.SaveChanges() != 0;
        }

        public ICollection<VacationDto> GetAllVacationsByUserId(int userId)
        {
            User user = _users.FirstOrDefault(u => u.UserId == userId) ?? throw new UserNotFoundException(userId);
            
            var dto = user.Vacations.Select(v => new VacationDto
            {
                VacationId = v.VacationId,
                AddedOn = v.AddedOn,
                DateDeparture = v.DateDeparture,
                DateReturn = v.DateReturn,
                Locations = (ICollection<VacationLocationDto>)v.Locations
            }).OrderByDescending(v => v.AddedOn).ToList();
            return dto;

        }

        public Vacation GetVacationById(int vacationId)
        {
            return _vacations.Where(v => v.VacationId == vacationId).FirstOrDefault() ?? throw new VacationNotFoundException(vacationId);
            
        }

        public bool UpdateVacation(int vacationId, Vacation vacation)
        {
            var vac = _vacations.Where(v => v.VacationId == vacationId).FirstOrDefault() ?? throw new VacationNotFoundException(vacationId);
            _vacations.Update(vac);
            return _context.SaveChanges() != 0;
        }
    }
}
