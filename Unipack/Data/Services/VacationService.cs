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

    }
}
