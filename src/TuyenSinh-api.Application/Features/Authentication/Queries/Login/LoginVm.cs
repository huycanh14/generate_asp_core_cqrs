using System;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Application.Features.Authentication.Queries.Login
{
    public class LoginVm
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public Role Role { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public int ExpireTime { get; set; }
    }
}
