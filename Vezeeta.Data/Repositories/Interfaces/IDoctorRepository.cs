﻿using Vezeeta.Core.Models;
using Vezeeta.Core.Parameters;
using Vezeeta.Data.Helpers;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<PagedList<Doctor>> GetAllDoctorWithPagination(DoctorParameters doctorParameters);
}
