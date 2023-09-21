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
            // show time as yyyy-MM-dd HH:mm:ss
            CreatedAt = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddMinutes(15)
        };
    }
}