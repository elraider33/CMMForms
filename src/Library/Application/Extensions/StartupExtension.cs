using System;
using System.Reflection;
using AutoMapper;
using Library.Application.Profiles;
using Library.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            // Automapper configuration
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BulletinProfile>();
                cfg.AddProfile<CMMProfile>();
                cfg.AddProfile<EntityProfile>();
                cfg.AddProfile<RoleProfile>();
            });

            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton(mapper);
        }
    }
}