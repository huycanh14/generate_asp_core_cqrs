using System;
using System.Collections.Generic;
using System.Text;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Persistence.Seeds.Identity
{
    public class DefaultUser
    {
        public static List<User> IdentityUserList()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    UserName = "Canh Huy",
                    Password = "123321",
                    RoleId = (int)RoleEnum.ADMIN,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    UserName = "Nhan vien",
                    Password = "123321",
                    RoleId = (int)RoleEnum.EMPLOYEE,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 3,
                    UserName = "Khách hàng",
                    Password = "123321",
                    RoleId = (int)RoleEnum.USER,
                    CreatedAt = DateTime.Now
                },
            };
        }
    }
}
