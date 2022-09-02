using System;
using System.Collections.Generic;

namespace TuyenSinh_api.Domain.Common
{
    public class DetailResponse<T>
    {
        public T DetailData { get; set; }
        public IEnumerable<HeaderTableVm> ListHeader { get; set; }

        public DetailResponse()
        {
            DetailData = default;
            ListHeader = new List<HeaderTableVm> { };
        }

        public DetailResponse(T data, IEnumerable<HeaderTableVm> listHeader)
        {
            DetailData = data;
            ListHeader = listHeader;
        }
    }
}
