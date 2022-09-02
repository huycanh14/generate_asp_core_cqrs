﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TuyenSinh_api.Persistence.Entities;

namespace TuyenSinh_api.Persistence.Migrations
{
    [DbContext(typeof(CleanArchitectureContext))]
    [Migration("20220813092145_DataUserMigrate")]
    partial class DataUserMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TuyenSinh_api.Domain.Entities.DotDangKy", b =>
                {
                    b.Property<int>("DotDangKyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DotDangKyID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AdmissionEndDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("AdmissionStartDate")
                        .HasColumnType("date");

                    b.Property<int?>("ChiTieu")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DieuKien")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("HeDaoTaoId")
                        .HasColumnName("HeDaoTaoID")
                        .HasColumnType("int");

                    b.Property<int?>("LePhiXetTuyen")
                        .HasColumnType("int");

                    b.Property<string>("LoaiDot")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("MaDot")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("date");

                    b.Property<DateTime?>("NgayCongBo")
                        .HasColumnType("date");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("TenDot")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<byte?>("ThuTu")
                        .HasColumnType("tinyint");

                    b.HasKey("DotDangKyId");

                    b.ToTable("DotDangKy");
                });

            modelBuilder.Entity("TuyenSinh_api.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Role Admin",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Role Employee",
                            Name = "Employee"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Role User",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("TuyenSinh_api.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleId")
                        .HasColumnType("int")
                        .HasDefaultValueSql("USER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 8, 13, 16, 21, 45, 66, DateTimeKind.Local).AddTicks(3822),
                            Password = "123321",
                            RoleId = 1,
                            UserName = "Canh Huy"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2022, 8, 13, 16, 21, 45, 68, DateTimeKind.Local).AddTicks(767),
                            Password = "123321",
                            RoleId = 2,
                            UserName = "Nhan vien"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2022, 8, 13, 16, 21, 45, 68, DateTimeKind.Local).AddTicks(899),
                            Password = "123321",
                            RoleId = 3,
                            UserName = "Khách hàng"
                        });
                });

            modelBuilder.Entity("TuyenSinh_api.Domain.Entities.User", b =>
                {
                    b.HasOne("TuyenSinh_api.Domain.Entities.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_Role");
                });
#pragma warning restore 612, 618
        }
    }
}
