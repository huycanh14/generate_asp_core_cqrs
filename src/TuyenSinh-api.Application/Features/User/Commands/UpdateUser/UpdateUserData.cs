using System;
using TuyenSinh_api.Application.Features.Common.Commands.Update;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserData : CommonDataUpdate
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; } = (int)RoleEnum.USER;
    }
}
