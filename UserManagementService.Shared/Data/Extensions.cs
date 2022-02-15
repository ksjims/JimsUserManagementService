using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagementService.Shared.Data;

public static class Extensions
{
    public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services, string connString)
            where TDbContext : DbContext, IDbFacadeResolver
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(connString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());

        services.AddHostedService<DbContextMigratorHostedService>();

        //doMoreActions?.Invoke(services);

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(repoType)
            .AddClasses(classes =>
                classes.AssignableTo(repoType)).As(typeof(IRepository<>)).WithScopedLifetime()
        );
        
        return services;
    }

    public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
    {
        var assembly = Assembly.GetCallingAssembly();
        var files = assembly.GetManifestResourceNames();
        var filePrefix = $"{assembly.GetName().Name}.Data.Scripts."; //IMPORTANT

        foreach (var file in files
            .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
            .Select(f => new { PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty) })
            .OrderBy(f => f.LogicalFile))
        {
            using var stream = assembly.GetManifestResourceStream(file.PhysicalFile);
            using var reader = new StreamReader(stream!);
            var command = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(command))
                continue;

            migrationBuilder.Sql(command);
        }
    }
}
