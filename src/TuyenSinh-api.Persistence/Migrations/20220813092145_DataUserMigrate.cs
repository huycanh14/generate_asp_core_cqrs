using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuyenSinh_api.Persistence.Migrations
{
    public partial class DataUserMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Password", "RoleId", "UpdatedAt", "UserName" },
                values: new object[] { 1, new DateTime(2022, 8, 13, 16, 21, 45, 66, DateTimeKind.Local).AddTicks(3822), "123321", 1, null, "Canh Huy" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Password", "RoleId", "UpdatedAt", "UserName" },
                values: new object[] { 2, new DateTime(2022, 8, 13, 16, 21, 45, 68, DateTimeKind.Local).AddTicks(767), "123321", 2, null, "Nhan vien" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Password", "RoleId", "UpdatedAt", "UserName" },
                values: new object[] { 3, new DateTime(2022, 8, 13, 16, 21, 45, 68, DateTimeKind.Local).AddTicks(899), "123321", 3, null, "Khách hàng" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
