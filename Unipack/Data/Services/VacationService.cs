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
        private readonly DbSet<PackList> _vacationLists;
        private readonly DbSet<PackItem> _vacationItems;
        private readonly DbSet<Item> _items;
        private readonly DbSet<User> _users;

        public VacationService(Context context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _vacationLists = context.PackLists;
            _vacationItems = context.VacationItems;
            _items = context.Items;
            _users = context.UnipackUsers;
        }

        public bool AddPackListToVacation(int vacationId, PackListDto packListDto)
        {
            throw new NotImplementedException();
        }

        public bool AddVacation(VacationDto vacation)
        {
            throw new NotImplementedException();
        }

        public bool DeletePackListFromVacationById(int vacationId, int packListId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVacationById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<PackListDto> GetAllVacationsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<PackListDto> GetVacationById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVacation(int id, VacationDto model)
        {
            throw new NotImplementedException();
        }
    }
}
