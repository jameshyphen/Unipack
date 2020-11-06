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
        ICollection<VacationDto> GetAllVacationsByUserId(int userId);
        Vacation GetVacationById(int vacationId);
        bool AddVacation(Vacation vacation);
        bool UpdateVacation(int vacationId, Vacation vacation);
        bool DeleteVacation(int vacationId);
    }
}
