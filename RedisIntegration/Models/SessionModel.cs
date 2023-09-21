﻿using StackExchange.Redis;

namespace RedisIntegration.Models;


public class SessionModel
{
    public string SessionId { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    
    public long InformationId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public TimeSpan Expiration { get; set; }
}

public class SessionUpdateModel
{
    public long InformationId { get; set; } 
}
public class DbAndSessionKeyModel
{
    public IDatabase Db { get; set; } = null!;

    public string SessionKey { get; set; } = null!;
}