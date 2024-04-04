using System.Linq.Expressions;
using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Repositories.Interfaces;
using Vezeeta.Data.Utilities;

namespace Vezeeta.Data.Repositories.Implementation;

public class PaginationRepository<T>(ApplicationDbContext context) : BaseRepository<T>(context), IPaginationRepository<T> where T : class
{
    public async Task<PaginationResponse<T>> SearchWithPagination(PaginationParameters parameters, Expression<Func<T, bool>> condition, params string[] includes)
    {
        var query = ApplyCondition(GetAll(), condition);
        query = ApplyIncludes(query, includes);
        return await PaginationResponse<T>.PaginateAsync(query, parameters.PageNumber, parameters.PageSize);
    }
}
