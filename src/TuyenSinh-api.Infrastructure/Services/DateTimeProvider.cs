using System;
using TuyenSinh_api.Application.Common.Interfaces.Services;

namespace TuyenSinh_api.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
