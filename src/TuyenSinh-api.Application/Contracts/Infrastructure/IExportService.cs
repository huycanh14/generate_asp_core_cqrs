using System;
using System.Collections.Generic;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Contracts.Infrastructure
{
    public interface IExportService
    {
        byte[] ExportDataToExcel<T>(DataCsvExport<T> sheet) where T : CommonExportVm;
    }
}
