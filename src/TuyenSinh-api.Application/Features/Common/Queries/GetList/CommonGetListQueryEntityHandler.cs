using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.Common.Queries.GetList
{
    public class CommonGetListQueryEntityHandler<TEntity> : IRequestHandler<CommonGetListQueryEntity<TEntity>, BaseResponse<ListResponse<TEntity>>> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonGetListQueryEntityHandler<TEntity>> _logger;
        public CommonGetListQueryEntityHandler(
            ILogger<CommonGetListQueryEntityHandler<TEntity>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ListResponse<TEntity>>> Handle(CommonGetListQueryEntity<TEntity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonGetListQueryEntityHandler@Handle -- Start");
            var _filter = request.filter;
            var filter = _filter.GetFilterWhere();
            var data = await _unitOfWork.GetRepository<TEntity>().GetAllFilterAsync(
                fields: filter,
                memberNames: _filter.MemberNames,
                keySort: _filter.UnqualifiedFieldName,
                includes: _filter.Includes,
                sortDir: _filter.sort,
                pageSize: _filter.Rows,
                page: _filter.Page
              );

            var properties = typeof(TEntity).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.Name
            }).ToList();

            _logger.LogInformation("CommonGetListQueryEntityHandler@Handle -- End");
            return new BaseResponse<ListResponse<TEntity>>(
                data: new ListResponse<TEntity>(
                    data: data.ListData,
                    listHeader: properties,
                    count: data.Count),
                    resultCode: ResultCodeEnum.GET
            );
        }
    }
}
