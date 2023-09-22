using StackExchange.Redis;

namespace RedisIntegration.Models;


public class SessionModel
{
    public string SessionId { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    
    public long InformationId { get; set; }
    
    public List<string>? Roles { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? RefreshedAt { get; set; }
    
    public DateTime Expiration { get; set; }
}

public class SessionUpdateModel
{
    public long InformationId { get; set; } 
    
    public List<string>? Roles { get; set; }
}
public class DbAndSessionKeyModel
{
    public IDatabase Db { get; set; } = null!;

    public string SessionKey { get; set; } = null!;
}