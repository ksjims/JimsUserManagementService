using Microsoft.EntityFrameworkCore;

namespace UserManagementService.Shared.Infrastructure.Data;

public abstract class BaseAppDbContext : DbContext, IDbFacadeResolver
{
    protected BaseAppDbContext(DbContextOptions options) : base(options)
    {
    }
}
