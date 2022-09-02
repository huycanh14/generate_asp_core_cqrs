using System;
namespace TuyenSinh_api.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public static string SectionName { get; } = "JwtSettings";
        public string Secret { get; set; } = null!;
        public int ExpiryDays { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
