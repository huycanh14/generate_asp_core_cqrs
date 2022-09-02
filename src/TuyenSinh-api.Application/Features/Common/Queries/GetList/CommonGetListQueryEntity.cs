using System;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Queries.GetList
{
    public class CommonGetListQueryEntity<TEntity> : IRequest<BaseResponse<ListResponse<TEntity>>> where TEntity : class
    {
        public FilterBase<TEntity> filter { get; set; }
    }
}
