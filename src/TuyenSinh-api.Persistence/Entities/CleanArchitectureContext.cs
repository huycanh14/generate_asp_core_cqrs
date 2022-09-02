using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Domain.Enum;
using TuyenSinh_api.Persistence.Seeds.Application;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuyenSinh_api.Persistence.Entities
{
    public partial class CleanArchitectureContext : DbContext
    {
        public CleanArchitectureContext()
        {
        }

        public CleanArchitectureContext(DbContextOptions<CleanArchitectureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<DotDangKy> DotDangKy { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CleanArchitecture_db;User=sa;Password=P@ssw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.Description).HasColumnName("Description");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("(getdate())"); ;

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.RoleId).HasColumnName("RoleId").HasDefaultValueSql($"{RoleEnum.USER}");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<DotDangKy>(entity =>
            {
                entity.Property(e => e.DotDangKyId).HasColumnName("DotDangKyID");

                entity.Property(e => e.AdmissionEndDate).HasColumnType("date");

                entity.Property(e => e.AdmissionStartDate).HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DieuKien).HasMaxLength(500);

                entity.Property(e => e.HeDaoTaoId).HasColumnName("HeDaoTaoID");

                entity.Property(e => e.LoaiDot).HasMaxLength(20);

                entity.Property(e => e.MaDot)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedTime).HasColumnType("smalldatetime");

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayCongBo).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TenDot)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.ApplicationSeed();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
