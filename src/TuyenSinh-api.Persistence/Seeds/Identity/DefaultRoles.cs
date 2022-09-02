using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Persistence.Seeds.Identity
{
    public class DefaultRoles
    {
        public static List<Role> IdentityRoleList()
        {
            return new List<Role>()
            {
                new Role
                {
                    Id = (int)RoleEnum.ADMIN,
                    Name = "Admin",
                    Description = "Role Admin"
                },
                new Role
                {
                    Id = (int)RoleEnum.EMPLOYEE,
                    Name = "Employee",
                    Description = "Role Employee"
                },
                new Role
                {
                    Id = (int)RoleEnum.USER,
                    Name = "User",
                    Description = "Role User"
                },
            };
        }
    }
}
