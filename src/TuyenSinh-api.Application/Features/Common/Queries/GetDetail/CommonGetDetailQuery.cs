using System;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Queries.GetDetail
{
    public class CommonGetDetailQuery<TVm, TEntity> : IRequest<BaseResponse<DetailResponse<TVm>>> where TEntity : class
    {
        public int Id { get; set; }
    }
}
