using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.Common.Commands.Delete
{
    public class CommonDeleteCommandHandler<TDto, TEntiy> : IRequestHandler<CommonDeleteCommand<TDto, TEntiy>, BaseResponse<TDto>>
        where TEntiy : class
        where TDto : class
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonDeleteCommandHandler<TDto, TEntiy>> _logger;

        public CommonDeleteCommandHandler(
            ILogger<CommonDeleteCommandHandler<TDto, TEntiy>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseResponse<TDto>> Handle(CommonDeleteCommand<TDto, TEntiy> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonDeleteCommandHandler@Handle -- Start");
            var entity = await _unitOfWork.GetRepository<TEntiy>().GetByIdAsync(request.Id);
            if (entity == null)
            {
                _logger.LogInformation("CommonDeleteCommandHandler@Handle -- End -- Not exist");
                throw new Exceptions.BadRequestException("Object does not exist");
            }
            _unitOfWork.GetRepository<TEntiy>().Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("CommonDeleteCommandHandler@Handle -- End");
            return new BaseResponse<TDto>(
                data: _mapper.Map<TDto>(entity),
                resultCode: ResultCodeEnum.DELETE
            );
        }
    }
}
