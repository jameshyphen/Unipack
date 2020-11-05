using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface IVacationListService
    {
        bool AddVacationList(VacationList list);

        //Task<VacationListDto> GetVacationListById(int id);

        //Task<IEnumerable<VacationListDto>> GetAllVacationListsByUser(int userId);

        bool DeleteVacationListById(int id);
        bool UpdateList(int id, VacationListDto model);

        bool AddItemToListByItemId(int itemId, int listId);

        bool DeleteItemFromListByVacationItemId(int vacationItemId, int listId);
        void getAllListsFromUser(User user);
    }
}
