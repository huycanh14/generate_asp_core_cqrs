using System;
using MediatR;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;
namespace TuyenSinh_api.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandCore : IRequest<BaseResponse<UserCreateDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; } = (int)RoleEnum.USER;
    }
}
