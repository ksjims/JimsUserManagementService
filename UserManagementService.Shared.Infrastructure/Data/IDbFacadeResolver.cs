using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UserManagementService.Shared.Infrastructure.Data;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}
