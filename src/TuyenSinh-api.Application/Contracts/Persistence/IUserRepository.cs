using System;
using System.Threading.Tasks;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Application.Contracts.Persistence
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByUserName(string username);

        void AddUser(User user);

        Task<bool> IsUserNameAsync(int id, string userName);
    }
}
