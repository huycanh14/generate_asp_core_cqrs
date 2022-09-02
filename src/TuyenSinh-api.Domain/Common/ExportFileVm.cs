using System;
namespace TuyenSinh_api.Domain.Common
{
    public class ExportFileVm
    {
        public string ExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
