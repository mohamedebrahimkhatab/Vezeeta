using Vezeeta.Core.Models;

namespace Vezeeta.Core.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);

    Task<User> Create(User user);
}
