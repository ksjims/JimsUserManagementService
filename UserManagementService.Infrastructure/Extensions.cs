using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.Infrastructure.Data;
using UserManagementService.Shared.Data;

namespace UserManagementService.Infrastructure;

public static class Extensions
{
    private const string DbName = "userManagementService";

    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddPostgresDbContext<AppDbContext>(config.GetConnectionString(DbName));

        return services;
    }
}
