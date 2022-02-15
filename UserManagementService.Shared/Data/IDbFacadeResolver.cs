using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UserManagementService.Shared.Data;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}
