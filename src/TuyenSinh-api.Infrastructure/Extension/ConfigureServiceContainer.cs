using System;
using Microsoft.Extensions.DependencyInjection;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Persistence.Repositories;

namespace TuyenSinh_api.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AppServicesRegistrar(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //// Repository
            //services.AddScoped<IDotDangKyRepository, DotDangKyRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
