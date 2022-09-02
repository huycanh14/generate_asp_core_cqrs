using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Application.Exceptions;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.Common.Queries.GetDetail
{
    public class CommonGetDetailQueryHandler<TVm, TEntity> : IRequestHandler<CommonGetDetailQuery<TVm, TEntity>, BaseResponse<DetailResponse<TVm>>> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonGetDetailQueryHandler<TVm, TEntity>> _logger;

        public CommonGetDetailQueryHandler(
            ILogger<CommonGetDetailQueryHandler<TVm, TEntity>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<DetailResponse<TVm>>> Handle(CommonGetDetailQuery<TVm, TEntity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonGetDetailQueryHandler@Handle -- Start");
            var id = request.Id;
            var data = await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(id: id);
            if (data == null)
            {
                _logger.LogInformation("CommonGetListQueryHandler@Handle -- End -- NotFound");
                throw new NotFoundException(nameof(TEntity), request.Id);
            }
            var dataVm = _mapper.Map<TVm>(data);
            PropertyInfo[] props = typeof(TVm).GetProperties();

            var properties = typeof(TVm).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && !p.IsDefined(typeof(JsonIgnoreAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.GetCustomAttributes(typeof(DisplayAttribute),
                        false).Cast<DisplayAttribute>().Single().Name
            }).ToList();

            _logger.LogInformation("CommonGetDetailQueryHandler@Handle -- End");
            return new BaseResponse<DetailResponse<TVm>>(
                data: new DetailResponse<TVm>(data: dataVm, listHeader: properties),
                resultCode: ResultCodeEnum.GET
            );
        }
    }
}
