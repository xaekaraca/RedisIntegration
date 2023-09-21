namespace RedisIntegration.Settings;

public class RedisSettings
{
    public string ConnectionString { get; set; } = null!;
    
    public string RedisPrefix { get; set; } = null!;
}