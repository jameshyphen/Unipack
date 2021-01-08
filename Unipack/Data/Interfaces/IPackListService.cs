using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface IPackListService
    {
        bool AddPackList(int vacationId, PackList list);

        PackList GetPackListById(int id);

        ICollection<PackList> GetAllPackListsByUser(int userId);

        bool DeletePackListById(int id);
        bool UpdatePackList(int id, PackListDto model);

        bool AddItemToPackList(PackItemDto model);

        bool DeleteItemFromListByItemId(int listId, int itemId);
    }
}
