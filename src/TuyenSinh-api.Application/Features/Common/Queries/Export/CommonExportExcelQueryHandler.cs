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
using TuyenSinh_api.Application.Contracts.Infrastructure;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Queries.Export
{
    public class CommonExportExcelQueryHandler<TVm, TEntity> : IRequestHandler<CommonExportExcelQuery<TVm, TEntity>, ExportFileVm>
        where TEntity : class
        where TVm : CommonExportVm
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExportService _exporter;
        private readonly ILogger<CommonExportExcelQueryHandler<TVm, TEntity>> _logger;

        public CommonExportExcelQueryHandler(
            ILogger<CommonExportExcelQueryHandler<TVm, TEntity>> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IExportService exportService
         )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _exporter = exportService;
        }

        public async Task<ExportFileVm> Handle(CommonExportExcelQuery<TVm, TEntity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CommonExportExcelQueryHandler@Handle -- Start");
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
            PropertyInfo[] props = typeof(TVm).GetProperties();

            var properties = typeof(TVm).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.GetCustomAttributes(typeof(DisplayAttribute),
                        false).Cast<DisplayAttribute>().Single().Name
            }).ToList();

            var fileData = _exporter.ExportDataToExcel(
                new DataCsvExport<TVm>
                {
                    RowsData = dataVm,
                    RowHeader = properties.ToArray(),
                    Title = request.Title
                });

            _logger.LogInformation("CommonExportExcelQueryHandler@Handle -- End");
            return new ExportFileVm
            {
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Data = fileData,
                ExportFileName = $"{typeof(TVm).Name}.xlsx"
            };
        }
    }
}
