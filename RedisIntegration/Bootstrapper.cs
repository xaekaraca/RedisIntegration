using RedisIntegration.Services;
using RedisIntegration.Settings;

namespace RedisIntegration;

public static class Bootstrapper
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisSettings>(
            configuration.GetSection("RedisSettings"));
        
        services.AddSingleton<SessionService>();
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("RedisSettings:ConnectionString").Value;
        });
    }
}