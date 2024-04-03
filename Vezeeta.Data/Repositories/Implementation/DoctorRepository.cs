using System.Linq.Expressions;
using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Data.Utilities;


namespace Vezeeta.Data.Repositories.Implementation;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context) { }
    public async Task<PaginationResponse<Doctor>> SearchDoctorsWithPagination(DoctorParameters doctorParameters, params string[] includes)
    {
        var query = ApplyCondition(GetAll(), e => doctorParameters.SpecializeId != 0 ? e.SpecializationId == doctorParameters.SpecializeId : true);
        query = ApplyCondition(query, e => e.ApplicationUser.FirstName.ToLower().Contains(doctorParameters.FirstName.ToLower()) ||
                                                e.ApplicationUser.LastName.ToLower().Contains(doctorParameters.LastName.ToLower()));
        query = ApplyIncludes(query, includes);
        return await PaginationResponse<Doctor>.PaginateAsync(query, doctorParameters.PageNumber, doctorParameters.PageSize);
    }
}
