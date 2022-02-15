using Microsoft.EntityFrameworkCore.Migrations;
using UserManagementService.Shared.Infrastructure.Data;

namespace UserManagementService.Infrastructure.Migrations
{
    public partial class SeedInitialUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.MigrateDataFromScript();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
