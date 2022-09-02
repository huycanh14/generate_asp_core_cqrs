using System;
using System.Collections.Generic;

namespace TuyenSinh_api.Domain.Common
{
    public class ListResponse<T>
    {
        public IEnumerable<T> ListData { get; set; }
        public IEnumerable<HeaderTableVm> ListHeader { get; set; }
        public int Count { get; set; }

        public ListResponse()
        {
            ListData = new List<T> { };
            ListHeader = new List<HeaderTableVm> { };
            Count = 0;
        }

        public ListResponse(IEnumerable<T> data, IEnumerable<HeaderTableVm> listHeader, int count = 0)
        {
            ListData = data;
            ListHeader = listHeader;
            Count = count;
        }
    }
}
