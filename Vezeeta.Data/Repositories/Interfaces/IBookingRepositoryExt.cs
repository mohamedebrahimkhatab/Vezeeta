namespace Vezeeta.Data.Repositories.Interfaces;

public interface IBookingRepositoryExt
{
    Task<IEnumerable<DateTime>> GetReserved();
}
