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
        private readonly DbSet<PackList> _packLists;
        private readonly DbSet<PackItem> _packItems;
        private readonly DbSet<Item> _items;
        private readonly DbSet<User> _users;
        private readonly DbSet<Vacation> _vacations;

        public PackListService(Context context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _packLists = context.PackLists;
            _vacations = context.Vacations;
            _packItems = context.PackItems;
            _items = context.Items;
            _users = context.UnipackUsers;
        }

        public PackList GetPackListById(int listId)
        {
            var packList = _packLists.FirstOrDefault(l => l.PackListId == listId) ?? throw new PackListNotFoundException(listId);
            return packList;
        }

        public ICollection<PackList> GetAllPackListsByUser(int userId)
        {
            User user = _users
                .Include(x => x.Vacations)
                .ThenInclude(x => x.PackLists)
                .ThenInclude(x => x.Items)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(u => u.UserId == userId) ?? throw new UserNotFoundException(userId);
            
            ICollection<PackList> packLists = user.Vacations.SelectMany(v=> v.PackLists).ToList();

            return packLists.OrderByDescending(l => l.AddedOn).ToList();
        }

        public bool AddItemToPackList(PackItemDto model)
        {
            Item item = _items.FirstOrDefault(i => i.ItemId == model.ItemId) ?? throw new ItemNotFoundException(model.ItemId);
            PackList list = _packLists.FirstOrDefault(l => l.PackListId == model.PackListId) ?? throw new PackListNotFoundException(model.PackListId);
            var vacationItem = new PackItem { Item = item, PackList = list, Quantity = model.Quantity};
            _packItems.Add(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool AddPackList(int vacationId, PackList list)
        {
            var vacation = _vacations.FirstOrDefault(x => x.VacationId == vacationId) ?? throw new VacationNotFoundException(vacationId);

            vacation.AddList(list);

            _packLists.Add(list);

            return _context.SaveChanges() != 0;
        }

        public bool DeletePackListById(int listId)
        {
            PackList list = _packLists.FirstOrDefault(l => l.PackListId == listId) ?? throw new PackListNotFoundException(listId);

            _packLists.Remove(list);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemFromListByItemId(int listId, int itemId)
        {
            PackList vacationList = _packLists
                .Include(x => x.Items)
                .FirstOrDefault(x => x.PackListId == listId) 
                ?? throw new PackListNotFoundException(listId);

            PackItem vacationItem = vacationList.Items.FirstOrDefault(x => x.ItemId == itemId) ??
                                        throw new ItemNotFoundException(itemId);

            vacationList.Items.Remove(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool UpdatePackList(int id, PackListDto model)
        {
            var list = _packLists.FirstOrDefault(l => l.PackListId == id) ?? throw new PackListNotFoundException(id);
            _packLists.Update(list);
            return _context.SaveChanges() != 0;
        }

        
    }
}
