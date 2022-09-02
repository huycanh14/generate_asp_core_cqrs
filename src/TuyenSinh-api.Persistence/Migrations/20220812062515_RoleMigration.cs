using Microsoft.EntityFrameworkCore.Migrations;

namespace TuyenSinh_api.Persistence.Migrations
{
    public partial class RoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "User",
                nullable: true,
                defaultValueSql: "USER");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { 1, "Role Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { 2, "Role Employee", "Employee" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[] { 3, "Role User", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");
        }
    }
}
