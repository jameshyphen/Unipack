using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class VacationListService : IVacationListService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<VacationList> _vacationLists;
        private readonly DbSet<VacationItem> _vacationItems;
        private readonly DbSet<Item> _items;

        public VacationListService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._vacationLists = context.VacationLists;
            this._vacationItems = context.VacationItems;
            this._items = context.Items;
        }

        
        public Task<VacationListDto> GetVacationListById(int id)
        {
            return _vacationLists
                .Where(l => l.VacationListId == id)
                .Select(l => new VacationListDto { })
                .FirstOrDefaultAsync();
        }

        //public Task<IEnumerable<VacationListDto>> GetAllVacationListsByUser(int userId)
        //{
        //}

        /// <summary>
        /// Adds an Item by Id to a List by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="listId"></param>
        /// <returns>boolean of any made changes</returns>
        public bool AddItemToListByItemId(int itemId, int listId)
        {
            var item = _items.Where(i => i.ItemId == itemId).FirstOrDefault();
            var list = _vacationLists.Where(l => l.VacationListId == listId).FirstOrDefault();
            var vacationItem = new VacationItem { Item = item, VacationList = list, Quantity = 1, AddedOn = DateTime.Now };
            _vacationItems.Add(vacationItem);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Add a VacationList to the database. Returns false if 0 changes are made
        /// </summary>
        /// <param name="list"></param>
        /// <returns>boolean of any made changes</returns>
        public bool AddVacationList(VacationList list)
        {
            _vacationLists.Add(list);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Delete a VacationList by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean of any made changes</returns>
        public bool DeleteVacationListById(int id)
        {
            var list = _vacationLists.Where(l => l.VacationListId == id).FirstOrDefault();
            _vacationLists.Remove(list);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Delete a Item by vacationItemId from List by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="vacationItemId"></param>
        /// <param name="listId"></param>
        /// <returns>boolean of any made changes</returns>
        public bool DeleteItemFromListByVacationItemId(int vacationItemId, int listId)
        {
            var vacationItem = _vacationItems.Where(vi => vi.ItemId == vacationItemId && vi.VacationListId == listId).FirstOrDefault();
            _vacationItems.Remove(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public void getAllListsFromUser(User user)
        {
            IList<VacationList> = _vacationLists.get
        }
    }
}
