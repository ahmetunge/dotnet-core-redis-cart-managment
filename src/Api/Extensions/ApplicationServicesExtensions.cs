using Business;
using Business.Validations;
using Business.Validations.FluentValidation;
using Core.Cache;
using Core.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<ICacheManager, RedisCache>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IBusinessCartValidation, BusinessCartValidation>();

            return services;
        }


        public static IServiceCollection AddRedisConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConfiguration>(configuration.GetSection("RedisConfiguration"));


            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var conf = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);

                return ConnectionMultiplexer.Connect(conf);
            });

            return services;

        }
    }
}