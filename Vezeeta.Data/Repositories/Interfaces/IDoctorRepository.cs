using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Utilities;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<PaginationResponse<Doctor>> SearchDoctorsWithPagination(DoctorParameters doctorParameters, params string[] includes);
}
