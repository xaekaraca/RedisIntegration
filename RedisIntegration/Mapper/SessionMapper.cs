using RedisIntegration.Models;

namespace RedisIntegration.Mapper;

public static class SessionMapper
{
    public static SessionModel ToSessionModel(UserModel user)
    {
        return new SessionModel
        {
            SessionId = Guid.NewGuid().ToString(),
            InformationId = user.InformationId,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            Expiration = TimeSpan.FromMinutes(15)
        };
    }
}