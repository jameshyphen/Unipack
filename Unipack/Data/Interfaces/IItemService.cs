using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.DTOs;

namespace Unipack.Data.Interfaces
{
    public interface IItemService
    {
        bool AddItem(ItemDto item);

        bool UpdateItem(int id, ItemDto item);

        bool AddCategory(string category);

        Task<ItemDto> GetItemById(int id);

        Task<IEnumerable<ItemDto>> GetAllItemsByUser(int userId);

        Task<IEnumerable<ItemDto>> GetAllItemsByCategory(string name);

        Task<IEnumerable<string>> GetAllCategoriesByUser(int userId);

        bool DeleteItemById(int id);

        bool DeleteCategoryByName(string name);

        bool AddItemToCategory(int id, string name);
    }
}
