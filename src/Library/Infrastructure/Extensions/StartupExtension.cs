using Library.Application.Services;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Library.Infrastructure.Helpers;
using Library.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Library.Infrastructure.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(
                configuration.GetSection(nameof(MongoOptions))
            );
            services.AddSingleton<IMongoOptions>(s =>
                s.GetRequiredService<IOptions<MongoOptions>>().Value);
            BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());
            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(configuration.GetSection("Settings"));
            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.AddSingleton(configuration);
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBulletinRepository, BulletinRepository>();
            services.AddScoped<ICMMRepository, CMMRepository>();
            services.AddScoped<IFileRepository, FormFileRepository>();
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFSChunkRepository, FSChunkRepository>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            return services;
        }
    }
}