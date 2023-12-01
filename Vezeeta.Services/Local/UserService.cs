using Vezeeta.Core;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Services.Local;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Create(User user)
    {
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAll() => await _unitOfWork.Users.GetAllAsync();

    public async Task<User?> GetById(int id) => await _unitOfWork.Users.GetByIdAsync(id);


}
