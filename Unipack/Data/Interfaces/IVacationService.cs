using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.DTOs;
using Unipack.Models;

namespace Unipack.Data.Interfaces
{
    public interface IVacationService
    {
        bool AddVacation(VacationDto vacation);

        Task<PackListDto> GetVacationById(int id);

        ICollection<PackListDto> GetAllVacationsByUser(int userId);

        bool DeleteVacationById(int id);
        bool UpdateVacation(int id, VacationDto model);

        bool AddPackListToVacation(int vacationId, PackListDto packListDto);

        bool DeletePackListFromVacationById(int vacationId, int packListId);
    }
}
