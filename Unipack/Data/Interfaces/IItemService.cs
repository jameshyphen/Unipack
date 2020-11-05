using System;
using System.Collections.Generic;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface IItemService
    {
        bool AddItem(Item item);

        bool UpdateItem(int itemId, Item item);

        Item GetItemById(int itemId);

        ICollection<Item> GetAllItemsByUserId(int userId);

        bool DeleteItemById(int itemId);

        bool AddItemToCategoryById(int itemId, int categoryId);
    }
}
