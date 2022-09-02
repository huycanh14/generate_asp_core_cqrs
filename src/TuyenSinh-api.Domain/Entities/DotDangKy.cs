using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuyenSinh_api.Domain.Entities
{
    public partial class DotDangKy
    {
        public int DotDangKyId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public string MaDot { get; set; }
        public string TenDot { get; set; }
        public string LoaiDot { get; set; }
        public int? HeDaoTaoId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime? NgayCongBo { get; set; }
        public int? ChiTieu { get; set; }
        public string DieuKien { get; set; }
        public int? LePhiXetTuyen { get; set; }
        public DateTime? AdmissionStartDate { get; set; }
        public DateTime? AdmissionEndDate { get; set; }
        public string Status { get; set; }
        public byte? ThuTu { get; set; }
    }
}
