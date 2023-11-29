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

    public async Task<User> Create(User patient)
    {
        await _unitOfWork.Users.AddAsync(patient);
        await _unitOfWork.CommitAsync();
        return patient;
    }

    public async Task<IEnumerable<User>> GetAll() => await _unitOfWork.Users.GetAllAsync();

    public async Task<User> GetById(int id) => await _unitOfWork.Users.GetByIdAsync(id);


}
