using System;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Commands.Create
{
    public class CommonCreateWithChangeDtoCommand<TValidator, TCreate, TDto, TEntiy> : IRequest<BaseResponse<CommonCreateDto>>
        where TEntiy : class
        where TDto : CommonCreateDto
        where TValidator : class
        where TCreate : CommonDataCreate
    {
        public TCreate DataCreate { get; set; }
    }
}
