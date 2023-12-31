﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedisIntegration.Mapper;
using RedisIntegration.Models;
using RedisIntegration.Settings;
using StackExchange.Redis;

namespace RedisIntegration.Services;

public class SessionService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly RedisSettings _redisSettings;
    private const int ExpireTime = 15;

    public SessionService(IOptions<RedisSettings> redisSettings)
    {
        _redis = ConnectionMultiplexer.Connect(redisSettings.Value.ConnectionString);
        _redisSettings = redisSettings.Value;
    }
    
    private DbAndSessionKeyModel GetRedisDbAndSessionKey(string userId)
    {
        return new DbAndSessionKeyModel
        {
            Db = _redis.GetDatabase(), 
            SessionKey = $"{_redisSettings.RedisPrefix}:{userId}:session"
        };
    }
    
    public SessionModel CreateSession(UserModel user)
    {
        var getRedisDbAndSessionKey = GetRedisDbAndSessionKey(user.Id);

        var session = SessionMapper.ToSessionModel(user, getRedisDbAndSessionKey.SessionKey);

        var jsonData = JsonConvert.SerializeObject(session);

        getRedisDbAndSessionKey.Db.StringSet(getRedisDbAndSessionKey.SessionKey, jsonData, TimeSpan.FromMinutes(ExpireTime));

        return session;
    }
    
    public SessionModel? GetSession(string userId)
    {
        var getRedisDbAndSessionKey = GetRedisDbAndSessionKey(userId);

        var jsonData = getRedisDbAndSessionKey.Db.StringGet(getRedisDbAndSessionKey.SessionKey);

        if (jsonData.IsNullOrEmpty) 
            return null;
        
        var session = JsonConvert.DeserializeObject<SessionModel>(jsonData);
        
        return session;
    }
    
    public SessionModel? UpdateAndRefreshSession(string userId, SessionUpdateModel sessionUpdateModel)
    {
        var getRedisDbAndSessionKey = GetRedisDbAndSessionKey(userId);
        
        var jsonData = getRedisDbAndSessionKey.Db.StringGet(getRedisDbAndSessionKey.SessionKey);
        
        if (jsonData.IsNullOrEmpty) 
            return null;
        
        var session = JsonConvert.DeserializeObject<SessionModel>(jsonData);
        
        session = SessionMapper.ToSessionModel(sessionUpdateModel, session!);
        
        jsonData = JsonConvert.SerializeObject(session);
        
        getRedisDbAndSessionKey.Db.StringSet(getRedisDbAndSessionKey.SessionKey, jsonData, TimeSpan.FromMinutes(ExpireTime));

        return session;
    }
    
    public void RefreshSession(string userId)
    {
        var getRedisDbAndSessionKey = GetRedisDbAndSessionKey(userId);
        
        var jsonData = getRedisDbAndSessionKey.Db.StringGet(getRedisDbAndSessionKey.SessionKey);
        
        if (jsonData.IsNullOrEmpty) 
            return;
        
        var session = JsonConvert.DeserializeObject<SessionModel>(jsonData);
        
        session!.Expiration = DateTime.UtcNow.AddMinutes(ExpireTime);
        session.RefreshedAt = DateTime.UtcNow;
        
        jsonData = JsonConvert.SerializeObject(session);
        
        getRedisDbAndSessionKey.Db.StringSet(getRedisDbAndSessionKey.SessionKey, jsonData, TimeSpan.FromMinutes(ExpireTime));
    }
    
    public void KillSession(string userId)
    {
        var getRedisDbAndSessionKey = GetRedisDbAndSessionKey(userId);
        
        getRedisDbAndSessionKey.Db.KeyDelete(getRedisDbAndSessionKey.SessionKey);
    }
}