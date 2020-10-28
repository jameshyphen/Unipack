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
            _itemCategories.Add(new ItemCategory() {Name = category, AddedOn = DateTime.Now});
            return _context.SaveChanges() != 0;
        }

        public bool AddItem(ItemDto item)
        {
            _items.Add(new Item() { Name = item.Name, AddedOn = item.AddedOn, ItemId = item.ItemId });
            return _context.SaveChanges() != 0;
        }

        public bool AddItemToCategory(int id, string name)
        {
            var category = _itemCategories.FirstOrDefault(c => c.Name == name);
            var item = _items.FirstOrDefault(i => i.ItemId == id);
            category.Items.Add(item);
            _itemCategories.Update(category);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteCategoryByName(string name)
        {
            var category = _itemCategories.FirstOrDefault(c => c.Name == name);
            _itemCategories.Remove(category);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemById(int id)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == id);
            _items.Remove(item);
            return _context.SaveChanges() != 0;
        }

        public Task<IEnumerable<string>> GetAllCategoriesByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsByCategory(string name)
        {
            var categoryId = _itemCategories.FirstOrDefault(c => c.Name == name).ItemCategoryID;
            return await _items.AsNoTracking().Where(i => i.CategoryId == categoryId)
                .Select(s => new ItemDto() {AddedOn = s.AddedOn, Category = s.Category.Name, ItemId = s.ItemId, Name = s.Name }).ToListAsync();
        }

        public Task<IEnumerable<ItemDto>> GetAllItemsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            return await _items.Where(i => i.ItemId == id)
                .Select(s => new ItemDto() { AddedOn = s.AddedOn, Category = s.Category.Name, ItemId = s.ItemId, Name = s.Name }).FirstOrDefaultAsync();
        }

        public bool UpdateItem(int id, ItemDto itemupdate)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == id);
            var updatedCategory = _itemCategories.FirstOrDefault(c => c.Name == itemupdate.Category);
            item.Name = itemupdate.Name;
            item.AddedOn = itemupdate.AddedOn;
            item.Category = updatedCategory;
            item.CategoryId = updatedCategory.ItemCategoryID;
            _items.Update(item);
            return _context.SaveChanges() != 0;
        }
    }
}
