using UserManagementService.Core.Interfaces;
using UserManagementService.Core.UserAggregate;

namespace UserManagementService.Core.Services
{
    public class UserService : IUserService
    {
        private readonly Dictionary<Guid, User> _users = new();

        public Task<bool> CreateAsync(User? user)
        {
            if (user is null)
            {
                return Task.FromResult(false);
            }

            _users[user.Id] = user;

            return Task.FromResult(true);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_users.GetValueOrDefault(id));
        }

        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(_users.Values.ToList());
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await GetByIdAsync(user.Id);

            if (existingUser is null)
            {
                return false;
            }

            _users[user.Id] = user;

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingUser = await GetByIdAsync(id);

            if (existingUser is null)
            {
                return false;
            }

            _users.Remove(id);

            return true;
        }
    }
}
