using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuyenSinh_api.Application.Common.Interfaces.Authentication;
using TuyenSinh_api.Application.Common.Interfaces.Services;
using TuyenSinh_api.Application.Contracts.Infrastructure;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Infrastructure.Authentication;
using TuyenSinh_api.Infrastructure.Extension;
using TuyenSinh_api.Infrastructure.Services;
using TuyenSinh_api.Persistence.Repositories;

namespace TuyenSinh_api.Infrastructure
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configration)
        {
            services.Configure<JwtSettings>(c => configration.GetSection(JwtSettings.SectionName).Bind(c));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IExportService, ExportService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton(ConfigureServiceMapper.AddServiceMapper());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
