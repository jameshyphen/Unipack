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
    public class ItemService : IItemService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<ItemCategory> _itemCategories;
        private readonly DbSet<Item> _items;

        public ItemService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._itemCategories = context.ItemCategories;
            this._items = context.Items;
        }

        public bool AddCategory(string category)
        {
            // TODO: User has to be added along with itemcategory aswell, find out who is logged in and add the corresponding user along
            //_itemCategories.Add(new ItemCategory(category));
            return _context.SaveChanges() != 0;
        }

        public bool AddItem(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public bool AddItemToCategory(int id, string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllCategoriesByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDto>> GetAllItemsByCategory(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDto>> GetAllItemsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto> GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(int id, ItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}
