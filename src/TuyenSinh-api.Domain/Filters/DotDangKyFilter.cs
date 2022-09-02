using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Domain.Filters
{
    //[BindProperties]
    public class DotDangKyFilter : FilterBase<DotDangKy>
    {
        public string LoaiDot { get; set; }
        public string TenDot { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        //public override string UnqualifiedFieldName
        //{
        //    get
        //    {
        //        PropertyInfo[] props = typeof(DotDangKy).GetProperties();
        //        var check = props.Where(x => KeySort != null && x.ToString().ToUpper()  == KeySort.ToUpper()).FirstOrDefault();
        //        if (check == null)
        //        {
        //            KeySort = "DotDangKyId";
        //        }
        //        else
        //        {
        //            KeySort = props.ToString();
        //        }
        //        return KeySort;
        //    }
        //}

        //public override List<Expression<Func<DotDangKy, bool>>> GetFilterWhere()
        //{
        //    var filterExpressions = new List<Expression<Func<DotDangKy, bool>>>();
        //    if (!string.IsNullOrEmpty(LoaiDot))
        //    {
        //        filterExpressions.Add(x => x.LoaiDot == LoaiDot);
        //    }
        //    if (!string.IsNullOrEmpty(TenDot))
        //    {
        //        filterExpressions.Add(x => x.TenDot.ToUpper().Contains(TenDot.ToUpper()));
        //    }
        //    if (NgayBatDau.HasValue)
        //    {
        //        filterExpressions.Add(x => x.NgayBatDau.HasValue && new DateTime(x.NgayBatDau.Value.Year, x.NgayBatDau.Value.Month, x.NgayBatDau.Value.Day, 0, 0, 0) <= NgayBatDau);
        //    }
        //    if (NgayKetThuc.HasValue)
        //    {
        //        filterExpressions.Add(x => x.NgayKetThuc.HasValue && new DateTime(x.NgayKetThuc.Value.Year, x.NgayKetThuc.Value.Month, x.NgayKetThuc.Value.Day, 23, 59, 59) >= NgayKetThuc);
        //    }
        //    return filterExpressions;
        //}
    }
}
