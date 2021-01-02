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
    public class PackListService : IPackListService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<PackList> _vacationLists;
        private readonly DbSet<PackItem> _vacationItems;
        private readonly DbSet<Item> _items;
        private readonly DbSet<User> _users;

        public PackListService(Context context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _vacationLists = context.VacationLists;
            _vacationItems = context.VacationItems;
            _items = context.Items;
            _users = context.UnipackUsers;
        }

        public Task<PackListDto> GetPackListById(int listId)
        {
            var vacationList = _vacationLists.Where(l => l.PackListId == listId) ?? throw new PackListNotFoundException(listId);
            return vacationList.Select(l => new PackListDto
                {
                    AddedOn = l.AddedOn,
                    Name = l.Name,
                    VacationListId = l.PackListId,
                    // TODO: Move this, change up VacationList to Vacation
                })
                .FirstOrDefaultAsync();
        }

        public ICollection<PackListDto> GetAllPackListsByUser(int userId)
        {
            User user = _users.FirstOrDefault(u => u.UserId == userId) ?? throw new UserNotFoundException(userId);
            ICollection<PackList> vacationLists = user.Vacations.SelectMany(v=> v.VacationLists).ToList();
            var dto = vacationLists.Select(list => new PackListDto
            {
                VacationListId = list.PackListId,
                AddedOn = list.AddedOn,
                Name = list.Name
                //Can be uncommented when VacationItemDto & VacationTaskDto have been implemented
                //Tasks = list.Tasks,
                //Items= list.Items
            }).OrderByDescending(l => l.AddedOn).ToList();
            return dto;
            

        }

        public bool AddItemToListByItemId(int listId, int itemId)
        {
            Item item = _items.FirstOrDefault(i => i.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);
            PackList list = _vacationLists.FirstOrDefault(l => l.PackListId == listId) ?? throw new PackListNotFoundException(listId);
            var vacationItem = new PackItem { Item = item, PackList = list, Quantity = 1, AddedOn = DateTime.Now };
            _vacationItems.Add(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool AddPackList(PackListDto list)
        {
            _vacationLists.Add(new PackList() { Name = list.Name, AddedOn = DateTime.Now });
            return _context.SaveChanges() != 0;
        }

        public bool DeletePackListById(int listId)
        {
            PackList list = _vacationLists.FirstOrDefault(l => l.PackListId == listId) ?? throw new PackListNotFoundException(listId);

            _vacationLists.Remove(list);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemFromListByVacationItemId(int itemId, int listId)
        {
            PackList vacationList = _vacationLists.FirstOrDefault(x => x.PackListId == listId) ??
                                        throw new PackListNotFoundException(listId);

            PackItem vacationItem = vacationList.Items.FirstOrDefault(x => x.ItemId == itemId) ??
                                        throw new ItemNotFoundException(itemId);

            vacationList.Items.Remove(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool UpdatePackList(int id, PackListDto model)
        {
            var list = _vacationLists.FirstOrDefault(l => l.PackListId == id) ?? throw new PackListNotFoundException(id);
            _vacationLists.Update(list);
            return _context.SaveChanges() != 0;
        }

        
    }
}
