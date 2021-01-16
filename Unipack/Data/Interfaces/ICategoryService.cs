using System.Collections.Generic;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategoryById(int categoryId);
        Category GetCategoryByIdWithItems(int categoryId);
        bool AddCategory(Category category);
        bool UpdateCategory(int categoryId, Category category);
        
        ICollection<Category> GetAllCategoriesByUserId(int userId);

        ICollection<Item> GetAllItemsByCategoryId(int categoryId);

        bool DeleteCategoryById(int categoryId);

        bool DeleteItemFromCategoryById(int itemId, int categoryId);

    }
}
