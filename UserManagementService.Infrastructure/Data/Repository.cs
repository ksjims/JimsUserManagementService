using UserManagementService.Shared.Core.Aggregate;
using UserManagementService.Shared.Infrastructure.Data;

namespace UserManagementService.Infrastructure.Data;

public class Repository<TEntity> : BaseRepository<AppDbContext, TEntity> where TEntity : BaseEntity
{
    public Repository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
