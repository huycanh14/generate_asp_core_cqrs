using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Persistence.Seeds.Identity
{
    public static class IdentityContextSeed
    {
        public static void IdentitySeed(this ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);
            CreateUsers(modelBuilder);
        }

        private static void CreateRoles(ModelBuilder modelBuilder)
        {
            List<Role> roles = DefaultRoles.IdentityRoleList();
            modelBuilder.Entity<Role>().HasData(roles);
        }
        private static void CreateUsers(ModelBuilder modelBuilder)
        {
            List<User> users = DefaultUser.IdentityUserList();
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
