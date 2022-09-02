using System;
using AutoMapper;
using TuyenSinh_api.Application.Features.TEntity.Queries.GetUsers;
using TuyenSinh_api.Application.Features.User.Commands.CreateUser;
using TuyenSinh_api.Application.Features.User.Commands.DeleteUser;
using TuyenSinh_api.Application.Features.User.Commands.UpdateUser;
using TuyenSinh_api.Application.Features.User.Dtos;
using TuyenSinh_api.Application.Features.User.Queries.ExportUser;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For User
            CreateMap<User, UserVm>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserData>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UpdateUserData>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserDeleteDto>().ReverseMap();
            CreateMap<User, UserExportVm>().ReverseMap();
            // _generate
        }
    }
}
