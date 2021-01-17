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
        bool AddVacation(VacationDto vacationDto, User user);
        VacationLocation AddLocation(int vacationId, VacationLocationDto location);

        Vacation GetVacationById(int id);

        ICollection<Vacation> GetAllVacationsByUser(int userId);

        bool DeleteVacationById(int id);
        bool UpdateVacation(int id, VacationDto model);
        bool UpdateVacationLocation(int vacationId, int vacationLocationId, VacationLocationDto model);
    }
}
