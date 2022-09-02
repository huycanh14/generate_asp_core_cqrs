using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.Common.Commands.Create
{
    public class CommonCreateWithChangrDtoCommandHandler<TValidator, TCreate, TDto, TEntiy> :
        IRequestHandler<CommonCreateWithChangeDtoCommand<TValidator, TCreate, TDto, TEntiy>, BaseResponse<CommonCreateDto>>
        where TEntiy : class
        where TDto : CommonCreateDto
        where TValidator : class
        where TCreate : CommonDataCreate
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonCreateWithChangeDtoCommand<TValidator, TCreate, TDto, TEntiy>> _logger;

        public CommonCreateWithChangrDtoCommandHandler(
            ILogger<CommonCreateWithChangeDtoCommand<TValidator, TCreate, TDto, TEntiy>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseResponse<CommonCreateDto>> Handle(CommonCreateWithChangeDtoCommand<TValidator, TCreate, TDto, TEntiy> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonCreateWithChangrDtoCommandHandler@Handle -- Start");
            var validator = Activator.CreateInstance(typeof(TValidator),
                  new object[] { _unitOfWork }) as AbstractValidator<TCreate>;
            var validationResult = await validator.ValidateAsync(request.DataCreate);

            if (validationResult.Errors.Count > 0)
            {
                _logger.LogInformation("CommonCreateWithChangrDtoCommandHandler@Handle -- End -- Validate");
                throw new Exceptions.ValidationException(validationResult);
            }
            var dataEntiy = _mapper.Map<TEntiy>(request.DataCreate);
            _unitOfWork.GetRepository<TEntiy>().Add(dataEntiy);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("CommonCreateWithChangrDtoCommandHandler@Handle -- End");
            return new BaseResponse<CommonCreateDto>(
                data: _mapper.Map<TDto>(dataEntiy),
                resultCode: ResultCodeEnum.CREATE
            );
        }
    }
}
