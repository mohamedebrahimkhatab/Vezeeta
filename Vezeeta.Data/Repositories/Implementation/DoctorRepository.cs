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
    public async Task<PaginationResponse<Doctor>> GetAllDoctorWithPagination(DoctorParameters doctorParameters)
    {
        //e => e.ApplicationUser.DateOfBirth.Year >= doctorParameters.MinYearOfBirth &&
        //                                                                e.ApplicationUser.DateOfBirth.Year <= doctorParameters.MaxYearOfBirth
        return await PaginationResponse<Doctor>.PaginateAsync(FindByCondition(e =>
                                                                            e.ApplicationUser.FirstName.ToLower().Contains(doctorParameters.FirstName.ToLower()) ||
                                                                            e.ApplicationUser.LastName.ToLower().Contains(doctorParameters.LastName.ToLower()))
                                                                            , doctorParameters.PageNumber, doctorParameters.PageSize);
    }

    public IQueryable<Doctor> FindByCondition(Expression<Func<Doctor, bool>> condition)
    {
        return _context.Set<Doctor>().Include(e => e.ApplicationUser).Include(e => e.Specialization).Where(condition);
    }
}
