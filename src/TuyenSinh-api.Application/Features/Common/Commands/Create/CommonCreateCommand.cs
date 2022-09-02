using System;
using FluentValidation;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Commands.Create
{
    public class CommonCreateCommand<TValidator, TDto, TEntiy> : IRequest<BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
        where TValidator : class
    {
        public TDto DataCreate { get; set; }
    }
}
