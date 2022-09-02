using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuyenSinh_api.Persistence.Migrations
{
    public partial class UserAndDotDangKyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DotDangKy",
                columns: table => new
                {
                    DotDangKyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "smalldatetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    MaDot = table.Column<string>(maxLength: 20, nullable: false),
                    TenDot = table.Column<string>(maxLength: 200, nullable: false),
                    LoaiDot = table.Column<string>(maxLength: 20, nullable: true),
                    HeDaoTaoID = table.Column<int>(nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "date", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "date", nullable: true),
                    NgayCongBo = table.Column<DateTime>(type: "date", nullable: true),
                    ChiTieu = table.Column<int>(nullable: true),
                    DieuKien = table.Column<string>(maxLength: 500, nullable: true),
                    LePhiXetTuyen = table.Column<int>(nullable: true),
                    AdmissionStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    AdmissionEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    ThuTu = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DotDangKy", x => x.DotDangKyID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DotDangKy");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
