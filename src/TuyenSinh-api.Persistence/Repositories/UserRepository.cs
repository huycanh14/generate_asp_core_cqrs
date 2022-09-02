using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Entities;
using TuyenSinh_api.Persistence.Entities;

namespace TuyenSinh_api.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly CleanArchitectureContext _context;
        public UserRepository(CleanArchitectureContext context) : base(context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByUserName(string username)
        {
            return _context.User.SingleOrDefault(x => x.UserName == username);
        }

        public async Task<bool> IsUserNameAsync(int id, string userName)
        {
            return await _context.User.Where(x => x.UserName == userName && x.Id != id).CountAsync() > 0;
        }
    }
}
