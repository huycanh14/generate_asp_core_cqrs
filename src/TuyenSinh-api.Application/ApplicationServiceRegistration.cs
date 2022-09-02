using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TuyenSinh_api.Application.Features.Common.Commands.Create;
using TuyenSinh_api.Application.Features.Common.Commands.Delete;
using TuyenSinh_api.Application.Features.Common.Commands.Update;
using TuyenSinh_api.Application.Features.Common.Queries.Export;
using TuyenSinh_api.Application.Features.Common.Queries.GetDetail;
using TuyenSinh_api.Application.Features.Common.Queries.GetList;
using TuyenSinh_api.Application.Features.TEntity.Queries.GetUsers;
using TuyenSinh_api.Application.Features.User.Commands.CreateUser;
using TuyenSinh_api.Application.Features.User.Commands.DeleteUser;
using TuyenSinh_api.Application.Features.User.Commands.UpdateUser;
using TuyenSinh_api.Application.Features.User.Dtos;
using TuyenSinh_api.Application.Features.User.Queries.ExportUser;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            // exam woth change dto
            //services.AddTransient(
            //    typeof(IRequestHandler<CommonCreateWithChangeDtoCommand<CreateUserCommandValidator, CreateUserData, UserCreateDto, User>, BaseResponse<CommonCreateDto>>),
            //    typeof(CommonCreateWithChangrDtoCommandHandler<CreateUserCommandValidator, CreateUserData, UserCreateDto, User>));
            // User
            services.AddTransient(
                typeof(IRequestHandler<CommonGetListQuery<UserDto, User>, BaseResponse<ListResponse<UserDto>>>),
                typeof(CommonGetListQueryHandler<UserDto, User>));
            services.AddTransient(
                typeof(IRequestHandler<CommonGetDetailQuery<UserDto, User>, BaseResponse<DetailResponse<UserDto>>>),
                typeof(CommonGetDetailQueryHandler<UserDto, User>));
            services.AddTransient(
                typeof(IRequestHandler<CommonExportExcelQuery<UserExportVm, User>, ExportFileVm>),
                typeof(CommonExportExcelQueryHandler<UserExportVm, User>));
            services.AddTransient(
                typeof(IRequestHandler<CommonCreateCommand<CreateUserCommandValidator, UserDto, User>, BaseResponse<UserDto>>),
                typeof(CommonCreateCommandHandler<CreateUserCommandValidator, UserDto, User>));
            services.AddTransient(
                typeof(IRequestHandler<CommonUpdateCommand<CreateUserCommandValidator, UserDto, User>, BaseResponse<UserDto>>),
                typeof(CommonUpdateCommandHandler<CreateUserCommandValidator, UserDto, User>));
            services.AddTransient(
                typeof(IRequestHandler<CommonDeleteCommand<UserDto, User>, BaseResponse<UserDto>>),
                typeof(CommonDeleteCommandHandler<UserDto, User>));

            // _generate
            return services;
        }
    }

}
