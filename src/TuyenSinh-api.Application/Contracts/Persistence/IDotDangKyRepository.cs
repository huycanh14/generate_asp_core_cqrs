using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Application.Contracts.Persistence
{
    public interface IDotDangKyRepository : IBaseRepository<DotDangKy>
    {
        Task<IEnumerable<DotDangKy>> GetDotDangKiesAsync();
        Task<IEnumerable<DotDangKy>> GetDotDangKiesOrderByDescendingAsync();
        Task<DotDangKy> GetDotDangKyHocBaHTAsync();
        Task<IEnumerable<string>> GetLoaiDotsAsync();
    }
}
