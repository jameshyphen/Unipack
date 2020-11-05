using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unipack.Data.Interfaces;
using Unipack.Exceptions;
using Unipack.Models;

namespace Unipack.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly Context _context;
        private readonly ILogger _logger;
        private readonly DbSet<Category> _categories;
        private readonly DbSet<Item> _items;

        public CategoryService(Context context, ILogger<UserService> _logger)
        {
            this._context = context;
            this._logger = _logger;
            this._categories = context.Categories;
            this._items = context.Items;
        }

        public bool AddCategory(Category category)
        {
            _categories.Add(category);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteCategoryById(int categoryId)
        {
            Category category = _categories.FirstOrDefault(x => x.CategoryId == categoryId) ??
                                throw new CategoryNotFoundException(categoryId);
            _categories.Remove(category);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteItemFromCategoryById(int itemId, int categoryId)
        {
            Category category = _categories.FirstOrDefault(x => x.CategoryId == categoryId) ??
                                throw new CategoryNotFoundException(categoryId);
            Item item = category.Items.FirstOrDefault(x => x.ItemId == itemId) ??
                        throw new ItemNotFoundException(itemId);

            category.Items.Remove(item);

            return _context.SaveChanges() != 0;
        }

        public ICollection<Category> GetAllCategoriesByUserId(int userId)
        {
            // This is really not performant but we can index the db on userid for category table so that should be fine.
            ICollection<Category> userCategories = _categories.Where(x => x.AuthorUser.UserId == userId).ToList();
            return userCategories;
        }

        public ICollection<Item> GetAllItemsByCategoryId(int categoryId)
        {
            Category category = _categories.FirstOrDefault(x => x.CategoryId == categoryId) ??
                                throw new CategoryNotFoundException(categoryId);
            return category.Items;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId) ??
                           throw new CategoryNotFoundException(categoryId);
            return category;
        }
    }
}
