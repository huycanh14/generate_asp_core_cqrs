using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Persistence.Entities;

namespace TuyenSinh_api.Persistence.Repositories
{
    public class DotDangKyRepository : BaseRepository<DotDangKy>, IDotDangKyRepository
    {
        /***
          * Kế thừa cả IDotDangKyRepository và BaseRepository truyền vào name dbset
         */
        private readonly CleanArchitectureContext _context;
        public DotDangKyRepository(CleanArchitectureContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DotDangKy>> GetDotDangKiesAsync()
        {
            return await _context.DotDangKy.ToListAsync();
        }

        public Task<IEnumerable<DotDangKy>> GetDotDangKiesOrderByDescendingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DotDangKy> GetDotDangKyHocBaHTAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetLoaiDotsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
