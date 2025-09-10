using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLH.ApproveIQ.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "CreatedUserId", "CreatedOnUtc", "ModifiedOnUtc", "IsActive", "IsDeleted" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Bo", "Manry", "bo.manry@sparkhound.com", Guid.Empty, DateTime.UtcNow, DateTime.UtcNow, true, false },
                    { Guid.NewGuid(), "Derrick", "Helms", "derrick.helms@sparkhound.com", Guid.Empty, DateTime.UtcNow, DateTime.UtcNow, true, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Email",
                keyValues: new object[]
                {
                    "bo.manry@sparkhound.com",
                    "derrick.helms@sparkhound.com"
                });
        }
    }
}
