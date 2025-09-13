using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLH.ApproveIQ.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectReferenceToBuJobNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "Projects",
                newName: "BuJobNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ReferenceId",
                table: "Projects",
                newName: "IX_Projects_BuJobNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuJobNumber",
                table: "Projects",
                newName: "ReferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_BuJobNumber",
                table: "Projects",
                newName: "IX_Projects_ReferenceId");
        }
    }
}
