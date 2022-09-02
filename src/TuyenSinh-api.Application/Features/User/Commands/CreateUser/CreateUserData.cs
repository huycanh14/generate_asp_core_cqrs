using System;
using TuyenSinh_api.Application.Features.Common.Commands.Create;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.User.Commands.CreateUser
{
    public class CreateUserData : CommonDataCreate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; } = (int)RoleEnum.USER;
    }
}
