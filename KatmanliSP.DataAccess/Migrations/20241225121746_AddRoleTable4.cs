using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KatmanliSP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RolesUpdated");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesUpdated",
                table: "RolesUpdated",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_RolesUpdated_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "RolesUpdated",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_RolesUpdated_RoleId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesUpdated",
                table: "RolesUpdated");

            migrationBuilder.RenameTable(
                name: "RolesUpdated",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
