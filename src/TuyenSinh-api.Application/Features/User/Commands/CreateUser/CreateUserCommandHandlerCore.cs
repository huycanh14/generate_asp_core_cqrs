using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;
using UserEntity = TuyenSinh_api.Domain.Entities.User;

namespace TuyenSinh_api.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandHandlerCore : IRequestHandler<CreateUserCommandCore, BaseResponse<UserCreateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandlerCore> _logger;

        public CreateUserCommandHandlerCore(
            ILogger<CreateUserCommandHandlerCore> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserCreateDto>> Handle(CreateUserCommandCore request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateUserCommandHandler@Handle -- Start");
            var response = new BaseResponse<UserCreateDto>();
            var validator = new CreateUserCommandValidatorCore(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
                _logger.LogInformation("CreateUserCommandHandler@Handle -- End -- Validate");
                throw new Exceptions.ValidationException(validationResult);
            }
            var dataEntiy = _mapper.Map<UserEntity>(request);
            _unitOfWork.GetRepository<UserEntity>().Add(dataEntiy);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("CreateUserCommandHandler@Handle -- End");
            return new BaseResponse<UserCreateDto>(
                data: _mapper.Map<UserCreateDto>(dataEntiy),
                resultCode: ResultCodeEnum.CREATE
            );
        }

    }
}
