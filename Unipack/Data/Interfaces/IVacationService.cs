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

        Vacation GetVacationById(int id);

        ICollection<Vacation> GetAllVacationsByUser(int userId);

        bool DeleteVacationById(int id);
        bool UpdateVacation(int id, VacationDto model);
    }
}
