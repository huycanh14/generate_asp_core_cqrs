using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Features.Common.Queries.GetList
{
    public class CommonGetListQueryHandler<TVm, TEntity> : IRequestHandler<CommonGetListQuery<TVm, TEntity>, BaseResponse<ListResponse<TVm>>> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommonGetListQueryHandler<TVm, TEntity>> _logger;

        public CommonGetListQueryHandler(
            ILogger<CommonGetListQueryHandler<TVm, TEntity>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
         )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ListResponse<TVm>>> Handle(CommonGetListQuery<TVm, TEntity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonGetListQueryHandler@Handle -- Start");
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
            var dataVm = _mapper.Map<IEnumerable<TVm>>(data.ListData);

            var properties = typeof(TVm).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && !p.IsDefined(typeof(JsonIgnoreAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.GetCustomAttributes(typeof(DisplayAttribute),
                        false).Cast<DisplayAttribute>().Single().Name
            }).ToList();

            _logger.LogInformation("CommonGetListQueryHandler@Handle -- End");
            return new BaseResponse<ListResponse<TVm>>(
                data: new ListResponse<TVm>(data: dataVm, listHeader: properties, count: data.Count),
                resultCode: ResultCodeEnum.GET
            );
        }
    }
}
