using System;
using MediatR;
using TuyenSinh_api.Domain.Common;
namespace TuyenSinh_api.Application.Features.Common.Commands.Update
{
    public class CommonUpdateWithChangeDtoCommand<TValidator, TUpdate, TDto, TEntiy> : IRequest<BaseResponse<CommonUpdateDto>>
        where TEntiy : class
        where TDto : CommonUpdateDto
        where TValidator : class
        where TUpdate : CommonDataUpdate
    {
        public TUpdate DataUpdate { get; set; }
        public int Id { get; set; }
    }
}
