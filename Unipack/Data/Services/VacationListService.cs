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
    public class VacationListService : IVacationListService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<VacationList> _vacationLists;
        private readonly DbSet<VacationListItem> _vacationItems;
        private readonly DbSet<Item> _items;
        private readonly DbSet<User> _users;

        public VacationListService(Context context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
            _vacationLists = context.VacationLists;
            _vacationItems = context.VacationItems;
            _items = context.Items;
            _users = context.UnipackUsers;
        }

        public Task<VacationListDto> GetVacationListById(int listId)
        {
            var vacationList = _vacationLists.Where(l => l.VacationListId == listId) ?? throw new VacationListNotFoundException(listId);
            return vacationList.Select(l => new VacationListDto
                {
                    AddedOn = l.AddedOn,
                    Name = l.Name,
                    VacationListId = l.VacationListId,
                    // TODO: Move this, change up VacationList to Vacation
                })
                .FirstOrDefaultAsync();
        }

        public ICollection<VacationListDto> GetAllVacationListsByUser(int userId)
        {
            User user = _users.FirstOrDefault(u => u.UserId == userId) ?? throw new UserNotFoundException(userId);
            ICollection<VacationList> vacationLists = user.Vacations.SelectMany(v=> v.VacationLists).ToList();
            var dto = vacationLists.Select(list => new VacationListDto
            {
                VacationListId = list.VacationListId,
                AddedOn = list.AddedOn,
                Name = list.Name
                //Can be uncommented when VacationItemDto & VacationTaskDto have been implemented
                //Tasks = list.Tasks,
                //Items= list.Items
            }).OrderByDescending(l => l.AddedOn).ToList();
            return dto;
            

        }

        public bool AddItemToListByItemId(int itemId, int listId)
        {
            Item item = _items.FirstOrDefault(i => i.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);
            VacationList list = _vacationLists.FirstOrDefault(l => l.VacationListId == listId) ?? throw new VacationListNotFoundException(listId);
            var vacationItem = new VacationListItem { Item = item, VacationList = list, Quantity = 1, AddedOn = DateTime.Now };
            _vacationItems.Add(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool AddVacationList(VacationListDto list)
        {
            _vacationLists.Add(new VacationList() { Name = list.Name, AddedOn = DateTime.Now });
            return _context.SaveChanges() != 0;
        }

        public bool DeleteVacationListById(int listId)
        {
            VacationList list = _vacationLists.FirstOrDefault(l => l.VacationListId == listId) ?? throw new VacationListNotFoundException(listId);

            _vacationLists.Remove(list);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemFromListByVacationItemId(int itemId, int listId)
        {
            VacationList vacationList = _vacationLists.FirstOrDefault(x => x.VacationListId == listId) ??
                                        throw new VacationListNotFoundException(listId);

            VacationListItem vacationItem = vacationList.Items.FirstOrDefault(x => x.ItemId == itemId) ??
                                        throw new ItemNotFoundException(itemId);

            vacationList.Items.Remove(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateList(int id, VacationListDto model)
        {
            var list = _vacationLists.FirstOrDefault(l => l.VacationListId == id) ?? throw new VacationListNotFoundException(id);
            _vacationLists.Update(list);
            return _context.SaveChanges() != 0;
        }

        
    }
}
