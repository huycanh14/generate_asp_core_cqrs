using System;
using System.Collections.Generic;

namespace TuyenSinh_api.Domain.Common
{
    public class DataCsvExport<T> where T : CommonExportVm
    {
        public IEnumerable<T> RowsData { get; set; }
        public HeaderTableVm[] RowHeader { get; set; }
        public string Title { get; set; }
    }
}
