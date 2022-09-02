using System;
using System.Runtime.Serialization;

namespace TuyenSinh_api.Domain.Enum
{
    public enum SortEnum
    {
        [EnumMember(Value = "ASC")]
        ASC,
        [EnumMember(Value = "DESC")]
        DESC,
    }
}
