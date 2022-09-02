using System;
using System.Runtime.Serialization;

namespace TuyenSinh_api.Domain.Enum
{
    public enum LoaiDotEnum
    {
        [EnumMember(Value = "HOCBA")]
        HOCBA,
        [EnumMember(Value = "THPT")]
        THPT,
    }
}
