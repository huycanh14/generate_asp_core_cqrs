using System;
using System.IO;
using MediatR;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.Common.Queries.Export
{
    public class CommonExportExcelQuery<TVm, TEntity> : IRequest<ExportFileVm>
        where TEntity : class
        where TVm : class
    {
        public FilterBase<TEntity> filter { get; set; }
        public String Title { get; set; }
    }
}
