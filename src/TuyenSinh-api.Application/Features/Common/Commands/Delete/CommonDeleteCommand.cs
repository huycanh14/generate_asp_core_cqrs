using System;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Commands.Delete
{
    public class CommonDeleteCommand<TDto, TEntiy> : IRequest<BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
    {
        public int Id { get; set; }
    }
}
