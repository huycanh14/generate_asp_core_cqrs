using System;
using MediatR;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Filters;

namespace TuyenSinh_api.Application.Features.TEntity.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<BaseResponse<ListResponse<UserVm>>>
    {
        public UserFilter filter { get; set; }
    }
}
