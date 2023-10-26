using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskMaster.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                newName: "IX_Users_UserRoleId");

            migrationBuilder.AddColumn<string>(
                name: "RoleDetails",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "UserRoleId", "RoleDetails", "RoleName" },
                values: new object[,]
                {
                    { 1, "The Administratorrole  is the highest role you are able to obtain.", "Admin" },
                    { 2, "The Manager role is the second highest role you can obtain.", "Manager" },
                    { 3, "The QA testing is the third highest role you can obtain", "Quality Assurance Tester" },
                    { 4, "The Developer role is the fourth highest role you can obtain.", "Developer" },
                    { 5, "The IT tech is the fifth highest role you can obtain.", "IT Tech" },
                    { 6, "The client role is the lowest role.", "Client" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "Roles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "UserRoleId",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "RoleDetails",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
