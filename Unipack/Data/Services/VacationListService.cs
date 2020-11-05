﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Exceptions;
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
                .Select(l => new VacationListDto
                {
                    AddedOn = l.AddedOn,
                    Name = l.Name,
                    VacationListId = l.VacationListId,
                    // TODO: Move this, change up VacationList to Vacation
                })
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<VacationListDto>> GetAllVacationListsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds an Item by Id to a List by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="listId"></param>
        /// <returns>boolean of any made changes</returns>
        public bool AddItemToListByItemId(int itemId, int listId)
        {
            Item item = _items.FirstOrDefault(i => i.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);
            VacationList list = _vacationLists.FirstOrDefault(l => l.VacationListId == listId) ?? throw new VacationListNotFoundException(listId);
            var vacationItem = new VacationItem { Item = item, VacationList = list, Quantity = 1, AddedOn = DateTime.Now };
            _vacationItems.Add(vacationItem);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Add a VacationList to the database. Returns false if 0 changes are made
        /// </summary>
        /// <param name="list"></param>
        /// <returns>boolean of any made changes</returns>
        public bool AddVacationList(VacationListDto list)
        {
            _vacationLists.Add(new VacationList() {Name = list.Name, AddedOn = DateTime.Now });
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Delete a VacationList by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>boolean of any made changes</returns>
        public bool DeleteVacationListById(int listId)
        {
            VacationList list = _vacationLists.FirstOrDefault(l => l.VacationListId == listId) ?? throw new VacationListNotFoundException(listId);

            _vacationLists.Remove(list);
            return _context.SaveChanges() != 0;
        }

        /// <summary>
        /// Delete a Item by vacationItemId from List by Id. Returns false if 0 changes are made
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="listId"></param>
        /// <returns>boolean of any made changes</returns>
        public bool DeleteItemFromListByVacationItemId(int itemId, int listId)
        {
            VacationList vacationList = _vacationLists.FirstOrDefault(x => x.VacationListId == listId) ?? 
                                        throw new VacationListNotFoundException(listId);

            VacationItem vacationItem = vacationList.Items.FirstOrDefault(x => x.ItemId == itemId) ??
                                        throw new ItemNotFoundException(itemId);

            vacationList.Items.Remove(vacationItem);
            return _context.SaveChanges() != 0;
        }

        public void getAllListsFromUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateList(int id, VacationListDto model)
        {
            throw new NotImplementedException();
        }
    }
}
