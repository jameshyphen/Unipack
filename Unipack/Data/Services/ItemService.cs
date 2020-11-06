using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Unipack.Data.Interfaces;
using Unipack.Exceptions;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class ItemService : IItemService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<Category> _categories;
        private readonly DbSet<Item> _items;

        public ItemService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._categories = context.Categories;
            this._items = context.Items;
        }

        public bool AddItem(Item item)
        {
            _items.Add(item);
            return _context.SaveChanges() != 0;
        }

        public bool AddItemToCategoryById(int itemId, int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId) ??
                                throw new CategoryNotFoundException(categoryId);

            var item = _items.FirstOrDefault(x => x.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);

            category.Items.Add(item);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemById(int itemId)
        {
            var item = _items.FirstOrDefault(x => x.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);
            _items.Remove(item);
            return _context.SaveChanges() != 0;
        }

        public ICollection<Item> GetAllItemsByUserId(int userId)
        {
            var items = _items
                .Where(x => x.Author.UserId == userId)
                .Include(x => x.Category)
                .ToList();
            return items;
        }

        public Item GetItemById(int itemId)
        {
            var item = _items.FirstOrDefault(x => x.ItemId == itemId) ?? throw new ItemNotFoundException(itemId);
            return item;
        }

        public bool UpdateItem(int itemId, Item item)
        {
            var toBeUpdatedItem = _items.FirstOrDefault(x => x.ItemId == itemId) ??
                                  throw new ItemNotFoundException(itemId);
            toBeUpdatedItem.Name = item.Name;
            toBeUpdatedItem.Category = item.Category;
            toBeUpdatedItem.Priority = item.Priority;

            _items.Update(toBeUpdatedItem);
            return _context.SaveChanges() != 0;
        }
    }
}