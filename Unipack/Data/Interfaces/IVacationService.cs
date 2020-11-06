using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.DTOs;

namespace Unipack.Data.Interfaces
{
    public interface IVacationService
    {
        ICollection<VacationListDto> GetAllVacationsByUserId(int userId);
    }
}
