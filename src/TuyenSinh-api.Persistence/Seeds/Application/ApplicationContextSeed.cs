using System;
using Microsoft.EntityFrameworkCore;
using TuyenSinh_api.Persistence.Seeds.Identity;

namespace TuyenSinh_api.Persistence.Seeds.Application
{
    public static class ApplicationContextSeed
    {
        public static void ApplicationSeed(this ModelBuilder modelBuilder)
        {
            IdentityContextSeed.IdentitySeed(modelBuilder);
        }
    }
}
