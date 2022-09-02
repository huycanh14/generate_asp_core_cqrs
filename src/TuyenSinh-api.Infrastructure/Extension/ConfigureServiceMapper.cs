using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TuyenSinh_api.Application.Profiles;

namespace TuyenSinh_api.Infrastructure.Extension
{
    public static class ConfigureServiceMapper
    {
        public static IMapper AddServiceMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
