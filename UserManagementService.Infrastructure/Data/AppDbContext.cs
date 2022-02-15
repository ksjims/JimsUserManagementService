using Microsoft.EntityFrameworkCore;
using UserManagementService.Core.UserAggregate;
using UserManagementService.Shared.Infrastructure.Data;

namespace UserManagementService.Infrastructure.Data;

public class AppDbContext : BaseAppDbContext
{
    private const string Schema = "usermanagementservice";

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension(Constants.UuidGenerator);

        modelBuilder.Entity<User>().ToTable("users", Schema);
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Id).HasColumnType("uuid")
            .HasDefaultValueSql(Constants.UuidAlgorithm);
        modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(100);
    }
}
