using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KatmanliSP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Roles",
                newName: "DescriptionN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionN",
                table: "Roles",
                newName: "Description");
        }
    }
}
