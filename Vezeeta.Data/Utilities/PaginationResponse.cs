using Microsoft.EntityFrameworkCore;

namespace Vezeeta.Data.Utilities;

public class PaginationResponse<T> where T : class
{
    public PaginationData Pagination { get; set; } = null!;

    public IEnumerable<T> Data { get; set; } = null!;

    public PaginationResponse(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        Pagination = new PaginationData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
        Data = items;
    }

    public static async Task<PaginationResponse<T>> PaginateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginationResponse<T>(items, count, pageNumber, pageSize);
    }
}
