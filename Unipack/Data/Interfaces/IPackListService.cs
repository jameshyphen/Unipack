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
        bool AddPackList(PackListDto list);

        Task<PackListDto> GetPackListById(int id);

        ICollection<PackListDto> GetAllPackListsByUser(int userId);

        bool DeletePackListById(int id);
        bool UpdatePackList(int id, PackListDto model);

        bool AddItemToListByItemId(int listId, int itemId);

        bool DeleteItemFromListByVacationItemId(int vacationItemId, int listId);
    }
}
