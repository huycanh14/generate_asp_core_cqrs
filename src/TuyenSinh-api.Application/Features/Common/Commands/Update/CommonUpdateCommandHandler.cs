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

namespace TuyenSinh_api.Application.Features.Common.Commands.Update
{
    public class CommonUpdateCommandHandler<TValidator, TDto, TEntiy> : IRequestHandler<CommonUpdateCommand<TValidator, TDto, TEntiy>, BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
        where TValidator : class
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonUpdateCommandHandler<TValidator, TDto, TEntiy>> _logger;

        public CommonUpdateCommandHandler(
            ILogger<CommonUpdateCommandHandler<TValidator, TDto, TEntiy>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseResponse<TDto>> Handle(CommonUpdateCommand<TValidator, TDto, TEntiy> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonUpdateCommandHandler@Handle -- Start");
            var exist = await _unitOfWork.GetRepository<TEntiy>().ExitsByIdAsync(request.Id);
            if (!exist)
            {
                _logger.LogInformation("UpdateUserCommandHandler@Handle -- End -- Not exist");
                throw new Exceptions.BadRequestException("Object does not exist");
            }
            var validator = Activator.CreateInstance(typeof(TValidator),
                  new object[] { _unitOfWork }) as AbstractValidator<TDto>;
            var validationResult = await validator.ValidateAsync(request.DataUpdate);

            if (validationResult.Errors.Count > 0)
            {
                _logger.LogInformation("UpdateUserCommandHandler@Handle -- End -- Validate");
                throw new Exceptions.ValidationException(validationResult);
            }
            var dataEntiy = _mapper.Map<TEntiy>(request.DataUpdate);
            _unitOfWork.GetRepository<TEntiy>().Update(dataEntiy);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("CommonUpdateCommandHandler@Handle -- End");
            return new BaseResponse<TDto>(
                data: _mapper.Map<TDto>(dataEntiy),
                resultCode: ResultCodeEnum.UPDATE
            );
        }
    }
}
