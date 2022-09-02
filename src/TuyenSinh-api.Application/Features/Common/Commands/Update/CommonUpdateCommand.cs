using System;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Commands.Update
{
    public class CommonUpdateCommand<TValidator, TDto, TEntiy> : IRequest<BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
        where TValidator : class
    {
        public TDto DataUpdate { get; set; }
        public int Id { get; set; }
    }
}
