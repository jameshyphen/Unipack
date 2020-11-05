using System.Collections.Generic;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface ICategoryService
    {
        bool AddCategory(Category category);
        
        ICollection<Category> GetAllCategoriesByUserId(int userId);

        ICollection<Item> GetAllItemsByCategoryId(int categoryId);

        bool DeleteCategoryById(int categoryId);

        bool DeleteItemFromCategoryById(int itemId, int categoryId);

    }
}
