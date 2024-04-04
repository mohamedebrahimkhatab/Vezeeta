using System.Linq.Expressions;
using Vezeeta.Core.Models;
using Vezeeta.Data.Parameters;
using Vezeeta.Data.Utilities;

namespace Vezeeta.Data.Repositories.Interfaces;

public interface IPaginationRepository<T> : IBaseRepository<T> where T : class
{
    Task<PaginationResponse<T>> SearchWithPagination(PaginationParameters doctorParameters, Expression<Func<T, bool>> condition,params string[] includes);
}
