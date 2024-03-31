using Vezeeta.Core.Models;
using Vezeeta.Core.Parameters;
using Vezeeta.Data.Helpers;
using Vezeeta.Data.Repositories.Interfaces;

namespace Vezeeta.Data.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context) { }
    public async Task<PagedList<Doctor>> GetAllDoctorWithPagination(DoctorParameters doctorParameters)
    {
        return await PagedList<Doctor>.ToPagedListAsync(GetAll(), doctorParameters.PageNumber, doctorParameters.PageSize);
    }
}
