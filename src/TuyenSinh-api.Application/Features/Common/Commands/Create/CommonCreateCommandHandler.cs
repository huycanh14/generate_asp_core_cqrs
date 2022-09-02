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
    public class CommonCreateCommandHandler<TValidator, TDto, TEntiy> : IRequestHandler<CommonCreateCommand<TValidator, TDto, TEntiy>, BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
        where TValidator : class
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonCreateCommandHandler<TValidator, TDto, TEntiy>> _logger;

        public CommonCreateCommandHandler(
            ILogger<CommonCreateCommandHandler<TValidator, TDto, TEntiy>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseResponse<TDto>> Handle(CommonCreateCommand<TValidator, TDto, TEntiy> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonCreateCommandHandler@Handle -- Start");
            var validator = Activator.CreateInstance(typeof(TValidator),
                  new object[] { _unitOfWork }) as AbstractValidator<TDto>;
            var validationResult = await validator.ValidateAsync(request.DataCreate);

            if (validationResult.Errors.Count > 0)
            {
                _logger.LogInformation("CreateUserCommandHandler@Handle -- End -- Validate");
                throw new Exceptions.ValidationException(validationResult);
            }
            var dataEntiy = _mapper.Map<TEntiy>(request.DataCreate);
            _unitOfWork.GetRepository<TEntiy>().Add(dataEntiy);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("CommonCreateCommandHandler@Handle -- End");
            return new BaseResponse<TDto>(
                data: _mapper.Map<TDto>(dataEntiy),
                resultCode: ResultCodeEnum.CREATE
            );
        }
    }
}
