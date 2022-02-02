using UserManagementService.Core.UserAggregate;

namespace UserManagementService.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateAsync(User? user);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
